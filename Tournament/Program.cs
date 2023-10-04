using System.Diagnostics;

namespace Tournament;

class Program
{
    private static string[] clientIDs = new[]
    {
        "client1",
        "client2"
    };

    public static void Main(string[] args)
    {
        string exePath = Path.Combine(Directory.GetCurrentDirectory(), "Network.exe");
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = string.Join(" ", clientIDs),
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

            // Wait for the process to exit
            process.WaitForExit();

            // Process has exited
            int exitCode = process.ExitCode;
            Console.WriteLine("Process exited with exit code: " + exitCode);
        }
    }
}
