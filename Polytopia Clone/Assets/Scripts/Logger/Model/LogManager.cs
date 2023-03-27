using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LogManager
    {
        private static LogManager instance;
        public static LogManager Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogManager();
                    JsonLogger jsonLogger = JsonLogger.Instance;
                    LogManager.Instance.LogEvent += jsonLogger.LogToFile;
                }
                return instance;
            }
        }
        
        public event Action<JsonDataHolder> LogEvent;

        public void TriggerEvent(JsonDataHolder data)
        {
            LogEvent?.Invoke(data);
        }
    }
}
