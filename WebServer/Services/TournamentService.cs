using System.Diagnostics;
using WebServer.Data;
using WebServer.Model;

namespace WebServer.Services;

public class TournamentService
{
    private static readonly string ZIP_FILES_PATH = Path.Combine(Environment.CurrentDirectory, "Resources\\ZipFiles");
    private static readonly string MAP_FILE_PATH = Path.Combine(Environment.CurrentDirectory, "Resources\\Maps");
    private static readonly string NETWORK_ROOT_PATH = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)!.FullName, "Network");
    private static readonly string COMP_FILES_DEST = Path.Combine(NETWORK_ROOT_PATH, "Client\\ClientFiles\\Compressed");
    private static readonly string EXTR_FILES_DEST = Path.Combine(NETWORK_ROOT_PATH, "Client\\ClientFiles\\Extracted");
    private static readonly string MAP_FILE_DEST = Path.Combine(NETWORK_ROOT_PATH, "Maps");

    private DataContext _dataContext;

    public TournamentService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public event Action<int>? OnTournamentStarted;
    public event Action<Dictionary<string, int>, TournamentType>? OnTournamentCompleted;
    public void StartTournament(string map, List<string> zipFileNames, TournamentType tournamentType, int roundCount)
    {
        TournamentResult tournamentResult = new TournamentResult
        {
            DateTime = DateTime.Now,
            MapFileName = map,
            TournamentType = tournamentType.ToString(),
            Finished = false
        };

        _dataContext.TournamentResults.Add(tournamentResult);
        _dataContext.SaveChanges();

        OnTournamentStarted?.Invoke(zipFileNames.Count);
        CopyFilesToNetwork(map, zipFileNames);

        List<string> clientIDs = zipFileNames.Select(z => z.Split('.')[0]).ToList();
        Dictionary<string, int> results = new();
        if(tournamentType == TournamentType.League)
        {
            results = RunLeagueTournament(map, clientIDs, roundCount);
        }
        else
        {
            results = RunKnockoutTournament(map, clientIDs.ToList());
        }

        RemoveFilesFromNetwork(map, zipFileNames);
        // Task.Run(() => { Network.Client.ClientManager.RemoveImages(clientIDs.ToList()).Wait(); });

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

    private Dictionary<string, int> RunLeagueTournament(string map, List<string> clientIDs, int roundCount)
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
                    string matchWinner = clientIDs[i]; // RunGame(map, new[]{ clientIDs[i], clientIDs[j] });
                    points[matchWinner] += 1;
                }
            }
        }

        return points.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
    }

    private Dictionary<string, int> RunKnockoutTournament(string map, List<string> clientIDs)
    {
        List<string> players = new List<string>(clientIDs);
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
                string matchWinner = lobby[0]; // RunGame(map, lobby.ToArray());
                lobby.Remove(matchWinner);
                eliminated.AddRange(lobby);
            }
            if(players.Count == 4)
            {
                players.RemoveAll(p => eliminated.Contains(p));
                string mathWinner = eliminated[0]; // RunGame(map, eliminated.ToArray());
                eliminated.Remove(mathWinner);
                points[mathWinner] = 3;
                points[eliminated[0]] = 4;
            }
            else if(players.Count == 2)
            {
                players.RemoveAll(p => eliminated.Contains(p));
                points[players[0]] = 1;
                points[eliminated[0]] = 2;
            }
            else
            {
                foreach (var elim in eliminated)
                {
                    players.Remove(elim);
                    points[elim] = roundsCount + 3 - i;
                }
            }
        }

        return points.OrderBy(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
    }

    private string RunGame(string map, string[] clients)
    {
        string winner = "";
        string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Network.exe");
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = $"{map} {string.Join(" ", clients)}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using (Process process = new Process())
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
            process.WaitForExit();
        }
        return winner;
    }

    private string GetWinnerNameFromText(string text)
    {
        return text.Replace("Winner: ", "").Replace(' ', '_').ToLower();
    }

    private void CopyFilesToNetwork(string map, List<string> zipFileNames)
    {
        var mapDest = Path.Combine(MAP_FILE_DEST, map);
        if (!File.Exists(mapDest))
        {
            File.Copy(Path.Combine(MAP_FILE_PATH, map), mapDest);
        }

        foreach (var zipFile in zipFileNames)
        {
            var zipDest = Path.Combine(COMP_FILES_DEST, zipFile);
            if (!File.Exists(zipDest))
            {
                File.Copy(Path.Combine(ZIP_FILES_PATH, zipFile), zipDest);
            }
        }
    }

    private void RemoveFilesFromNetwork(string map, List<string> zipFileNames)
    {
        var mapDest = Path.Combine(MAP_FILE_DEST, map);
        if (File.Exists(mapDest))
        {
            File.Delete(mapDest);
        }

        foreach (var zipFile in zipFileNames)
        {
            var compDest = Path.Combine(COMP_FILES_DEST, zipFile);
            if (File.Exists(compDest))
            {
                File.Delete(compDest);
            }

            var extrDest = Path.Combine(EXTR_FILES_DEST, zipFile.Split('.')[0]);
            if (Directory.Exists(compDest))
            {
                Directory.Delete(extrDest, true);
            }
        }
    }
}
