using Model;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using UnityEngine;

namespace View
{
    public class JsonLogger
    {
        private static JsonLogger instance = new JsonLogger();

        private JsonLogger()
        {
            LogManager.Instance.LogEvent += instance.LogToFile;
        }

        public static JsonLogger Instance { get => instance; }

        public void LogToFile(JsonDataHolder dataHolder)
        {
            string jsonString = JsonSerializer.Serialize(dataHolder);
            File.AppendAllText(Directory.GetCurrentDirectory() + @"\Assets\Log\playLog.txt", jsonString + "\n");
        }
    }
}
