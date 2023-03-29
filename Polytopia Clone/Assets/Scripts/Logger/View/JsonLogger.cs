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
                if(instance == null)
                {
                    instance = new JsonLogger();
                    Model.LogManager.Instance.LogEvent += instance.LogToFile;
                }
                return instance;
            }
        }

        public void LogToFile(Model.JsonDataHolder dataHolder)
        {
            string jsonString = JsonSerializer.Serialize(dataHolder);
            File.AppendAllText(Directory.GetCurrentDirectory() + @"\Assets\Log\playLog.txt", jsonString + "\n");
        }
    }
}
