using System.Diagnostics;

namespace WebServer.Services;

public class TournamentService
{
    public void StartTournament(string map, List<string> clientIDs)
    {
        List<string> players = new List<string>(clientIDs);

        for (int i = 0; i <= (int)Math.Log2(players.Count); i++)
        {
            List<string> eliminated = new();
            for (int j = 0; j < players.Count; j += 2)
            {
                List<string> lobby = new() { players[j], players[j + 1] };
                string winner = RunGame(map, lobby.ToArray());
                lobby.Remove(winner);
                eliminated.AddRange(lobby);
            }
            players.RemoveAll(p => eliminated.Contains(p));
        }
        Task.Run(() => { Network.Client.ClientManager.RemoveImages(clientIDs.ToList()).Wait(); });
    }

    private static string RunGame(string map, string[] clients)
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

    private static string GetWinnerNameFromText(string text)
    {
        return text.Replace("Winner: ", "").Replace(' ', '_').ToLower();
    }
}
