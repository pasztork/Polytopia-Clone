using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Model
{
    public class JsonLogger
    {
        private static JsonLogger instance;
        public static JsonLogger Instance
        {
            get
            {
                instance ??= new JsonLogger();
                return instance;
            }
        }

        public void LogToFile(JsonDataHolder dataHolder)
        {
            string jsonString = JsonSerializer.Serialize(dataHolder);
            File.AppendAllText(Directory.GetCurrentDirectory() + @"\Assets\Log\playLog.txt", jsonString + "\n");
        }
    }
}
