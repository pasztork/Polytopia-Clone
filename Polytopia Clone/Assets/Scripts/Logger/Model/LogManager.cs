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

        private static int buildId = 0;
        private static int tileId = 0;
        private static int troopId = 0;
        public int BuildId { get { return buildId; } }
        public int TroopId { get { return troopId; } }
        public int TileId { get { return buildId; } }

        public static LogManager Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogManager();
                    JsonLogger logger = JsonLogger.Instance;
                    LogManager.Instance.LogEvent += logger.LogToFile;
                }
                return instance;
            }
        }
        
        public event Action<JsonDataHolder> LogEvent;

        public void TriggerEvent(JsonDataHolder data)
        {
            LogEvent?.Invoke(data);
        }

        public int IncrementBuildId() => buildId++;
        public int IncrementTroopId() => troopId++;
        public int IncrementTileId() => tileId++;
    }
}
