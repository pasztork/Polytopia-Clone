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
        private static JsonLogger instance;

        public static JsonLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JsonLogger();
                    LogManager.Instance.LogEvent += instance.LogToFile;
                    LogManager.Instance.LogLastEvent += instance.LastLogToFile;
                    File.AppendAllText(Directory.GetCurrentDirectory() + @"\Assets\Log\playLog.txt", "[\n");
                }
                return instance;
            }
        }

        public void SetUpToLog()
        {
            JsonLogger js = JsonLogger.Instance;
        }

        public void LogToFile(JsonDataHolder dataHolder)
        {
            string jsonString = JsonSerializer.Serialize(dataHolder);
            File.AppendAllText(Directory.GetCurrentDirectory() + @"\Assets\Log\playLog.txt", jsonString + ",\n");
        }

        public void LastLogToFile(JsonDataHolder dataHolder)
        {
            string jsonString = JsonSerializer.Serialize(dataHolder);
            File.AppendAllText(Directory.GetCurrentDirectory() + @"\Assets\Log\playLog.txt", jsonString + "\n]");
        }
    }
}
