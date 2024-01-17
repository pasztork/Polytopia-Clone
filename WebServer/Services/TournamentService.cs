using System.Diagnostics;
using WebServer.Data;
using WebServer.Model;

namespace WebServer.Services;

public class TournamentService
{
    private static readonly string ZIP_FILES_PATH = Path.Combine(Environment.GetEnvironmentVariable("TERRA_IMPERIUM_PROJECT_ROOT")!, "WebServer\\ZipFiles");
    private static readonly string NETWORK_ROOT_PATH = Path.Combine(Environment.GetEnvironmentVariable("TERRA_IMPERIUM_PROJECT_ROOT")!, "Network");
    private static readonly string COMP_FILES_DEST = Path.Combine(NETWORK_ROOT_PATH, "Client\\ClientFiles\\Compressed");
    private static readonly string EXTR_FILES_DEST = Path.Combine(NETWORK_ROOT_PATH, "Client\\ClientFiles\\Extracted");

    private readonly DataContext _dataContext;

    public TournamentService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public event Action<int>? OnTournamentStarted;
    public event Action<Dictionary<string, int>, TournamentType>? OnTournamentCompleted;
    public event Action<List<string>>? OnMatchStarted;
    public event Action<string>? OnMatchCompleted;
    public event Action<Dictionary<string, int>, TournamentType>? OnPointsUpdated;

    public async Task StartTournament(string map, List<string> zipFileNames, TournamentType tournamentType, int roundCount, int maxTurnsInMatch)
    {
        TournamentResult tournamentResult = new()
        {
            DateTime = DateTime.Now,
            MapFileName = map,
            TournamentType = tournamentType.ToString(),
            Finished = false
        };

        _dataContext.TournamentResults.Add(tournamentResult);
        _dataContext.SaveChanges();

        OnTournamentStarted?.Invoke(zipFileNames.Count);
        CopyFilesToNetwork(zipFileNames);

        List<string> clientIDs = zipFileNames.Select(z => z.Split('.')[0]).ToList();
        Dictionary<string, int> results = new();
        if(tournamentType == TournamentType.League)
        {
            results = await RunLeagueTournament(map, clientIDs, roundCount, maxTurnsInMatch);
        }
        else
        {
            results = await RunKnockoutTournament(map, clientIDs, maxTurnsInMatch);
        }

        RemoveFilesFromNetwork(zipFileNames);
        await Network.Client.ClientManager.RemoveImages(clientIDs);

        tournamentResult.Finished = true;
        _dataContext.TournamentResults.Update(tournamentResult);
        foreach(var result in results)
        {
            _dataContext.ResultItems.Add(new ResultItem
            {
                FileName = result.Key,
                Value = result.Value,
                TournamentResultId = tournamentResult.Id
            });
        }
        _dataContext.SaveChanges();

        OnTournamentCompleted?.Invoke(results, tournamentType);
    }

    private async Task<Dictionary<string, int>> RunLeagueTournament(string map, List<string> clientIDs, int roundCount, int maxTurnsInMatch)
    {
        Dictionary<string, int> points = new();

        foreach (var clientID in clientIDs)
        {
            points.Add(clientID, 0);
        }

        for(int round = 1; round <= roundCount; round++)
        {
            for(int i = 0; i < clientIDs.Count - 1; i++)
            {
                for(int j = i + 1; j < clientIDs.Count; j++)
                {
                    List<string> lobby = new() { clientIDs[i], clientIDs[j] };
                    OnMatchStarted?.Invoke(lobby);
                    string matchWinner = await RunGame(map, maxTurnsInMatch, lobby.ToArray());
                    points[matchWinner] += 1;
                    OnMatchCompleted?.Invoke(matchWinner);
                    OnPointsUpdated?.Invoke(points, TournamentType.League);
                }
            }
        }

        return points.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
    }

    private async Task<Dictionary<string, int>> RunKnockoutTournament(string map, List<string> clientIDs, int maxTurnsInMatch)
    {
        List<string> players = new(clientIDs);
        Dictionary<string, int> points = new();

        foreach (var clientID in clientIDs)
        {
            points.Add(clientID, 0);
        }

        int roundsCount = (int)Math.Log2(clientIDs.Count);
        for (int i = 1; i <= roundsCount; i++)
        {
            List<string> eliminated = new();
            for (int j = 0; j < players.Count; j += 2)
            {
                List<string> lobby = new() { players[j], players[j + 1] };
                OnMatchStarted?.Invoke(lobby);
                string matchWinner = await RunGame(map, maxTurnsInMatch, lobby.ToArray());
                OnMatchCompleted?.Invoke(matchWinner);
                lobby.Remove(matchWinner);
                eliminated.AddRange(lobby);
            }
            if(players.Count == 4)
            {
                players.RemoveAll(p => eliminated.Contains(p));
                OnMatchStarted?.Invoke(eliminated);
                string matchWinner = await RunGame(map, maxTurnsInMatch, eliminated.ToArray());
                OnMatchCompleted?.Invoke(matchWinner);
                eliminated.Remove(matchWinner);
                points[matchWinner] = 3;
                points[eliminated[0]] = 4;
                OnPointsUpdated?.Invoke(points, TournamentType.Knockout);
            }
            else if(players.Count == 2)
            {
                players.RemoveAll(p => eliminated.Contains(p));
                points[players[0]] = 1;
                points[eliminated[0]] = 2;
                OnPointsUpdated?.Invoke(points, TournamentType.Knockout);
            }
            else
            {
                foreach (var elim in eliminated)
                {
                    players.Remove(elim);
                    points[elim] = roundsCount + 3 - i;
                    OnPointsUpdated?.Invoke(points, TournamentType.Knockout);
                }
            }
        }

        return points.OrderBy(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
    }

    private async Task<string> RunGame(string map, int maxTurnsInMatch, string[] clients)
    {
        string winner = "";
        string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Network.exe");
        ProcessStartInfo startInfo = new()
        {
            FileName = exePath,
            Arguments = $"{map} {maxTurnsInMatch} {string.Join(" ", clients)}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using (Process process = new())
        {
            process.StartInfo = startInfo;

            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    if (e.Data.Contains("Winner: "))
                    {
                        winner = GetWinnerNameFromText(e.Data);
                    }
                    Console.WriteLine("Output: " + e.Data);
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    Console.WriteLine("Error: " + e.Data);
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            await process.WaitForExitAsync();
        }
        return winner;
    }

    private string GetWinnerNameFromText(string text)
    {
        return text.Replace("Winner: ", "").Replace(' ', '_').ToLower();
    }

    private void CopyFilesToNetwork(List<string> zipFileNames)
    {
        foreach (var zipFile in zipFileNames)
        {
            var zipDest = Path.Combine(COMP_FILES_DEST, zipFile);
            if (!File.Exists(zipDest))
            {
                File.Copy(Path.Combine(ZIP_FILES_PATH, zipFile), zipDest);
            }
        }
    }

    private void RemoveFilesFromNetwork(List<string> zipFileNames)
    {
        foreach (var zipFile in zipFileNames)
        {
            var compDest = Path.Combine(COMP_FILES_DEST, zipFile);
            if (File.Exists(compDest))
            {
                File.Delete(compDest);
            }

            var extrDest = Path.Combine(EXTR_FILES_DEST, zipFile.Split('.')[0]);
            if (Directory.Exists(extrDest))
            {
                Directory.Delete(extrDest, true);
            }
        }
    }
}
