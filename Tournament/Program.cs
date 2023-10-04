using System.Diagnostics;

namespace Tournament;

class Program
{
    private static string[] clientIDs = new[]
    {
        "bob",
        "cersei"
    };

    public static void Main(string[] args)
    {
        string result = RunGame(clientIDs);
        Console.WriteLine(result);
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
                    if (e.Data.Contains("won the game"))
                    {
                        winner = e.Data.Split(" ")[0];
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
}
