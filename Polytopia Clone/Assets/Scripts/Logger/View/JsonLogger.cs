using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using UnityEngine;

namespace View
{
    public class JsonLogger : LoggerBase
    {
        private static JsonLogger instance;
        public JsonLogger()
        {
            Model.LogManager.Instance.LogEvent += instance.LogToFile;
        }

        public override void LogToFile(Model.JsonDataHolder dataHolder)
        {
            string jsonString = JsonSerializer.Serialize(dataHolder);
            File.AppendAllText(Directory.GetCurrentDirectory() + @"\Assets\Log\playLog.txt", jsonString + "\n");
        }
    }
}
