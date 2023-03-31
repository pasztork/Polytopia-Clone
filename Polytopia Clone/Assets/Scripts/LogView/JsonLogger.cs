using System;
using System.IO;

namespace View
{
    public class JsonLogger
    {
        private static readonly string saveDirectory = $"{Directory.GetCurrentDirectory()}\\GameLogs";
        private static readonly string filePath = $"{saveDirectory}\\{DateTime.Now:yyyy-mm-dd_hh-mm-ss}.json";

        public static JsonLogger Instance { get; } = new JsonLogger();

        private JsonLogger()
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            File.AppendAllText(filePath, "{}");
        }
    }
}
