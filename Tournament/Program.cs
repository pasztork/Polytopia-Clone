using System;
using System.Diagnostics;

namespace Tournament;

class Program
{
    private static string[] clientIDs = new[]
    { 
        "390498cdb1ccf066248b589ae001f13094e0d16707b2afbd69988c6f214b53cd",
        "2848ecf499e9da73b0911ce297cc7d46c755a879063ea39a2031daf0e418b6a3"
    };

    public static void Main(string[] args)
    {
        string exePath = "C:\\Users\\user\\Documents\\GitHub\\Polytopia-Clone\\Network\\bin\\Debug\\net6.0\\Network.exe";

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
