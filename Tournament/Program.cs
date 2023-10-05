using System.Diagnostics;

namespace Tournament;

class Program
{
    private static readonly string[] _clientIDs = new[]
    {
        "alice", "bob", "cersei", "two_named"
    };

    public static void Main(string[] args)
    {
        List<string> players = new List<string>(_clientIDs);

        for(int i = 0; i <= (int)Math.Log2(players.Count); i++)
        {
            List<string> eliminated = new();
            for(int j = 0; j < players.Count; j += 2)
            {
                List<string> lobby = new() { players[j], players[j + 1] };
                string winner = RunGame(lobby.ToArray());
                lobby.Remove(winner);
                eliminated.AddRange(lobby);
            }
            players.RemoveAll(p => eliminated.Contains(p));
        }
        Network.Client.ClientManager.RemoveImages(_clientIDs.ToList()).Wait();
    }

    private static string RunGame(string[] clients)
    {
        string winner = "";
        string exePath = Path.Combine(Directory.GetCurrentDirectory(), "Network.exe");
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = string.Join(" ", clients),
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
            int exitCode = process.ExitCode;
            Console.WriteLine("Process exited with exit code: " + exitCode);
        }
        return winner;
    }

    private static string GetWinnerNameFromText(string text)
    {
        return text.Replace("Winner: ", "").Replace(' ', '_').ToLower();
    }
}
