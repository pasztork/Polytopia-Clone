using Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace View
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
                    LogDataWrapper.Instance.NewDataCreated += LogManager.Instance.TriggerEvent;
                }
                return instance; 
            }
        }

        public event Action<JsonDataHolder> LogEvent;
        public void TriggerEvent(JsonDataHolder data)
        {
            LogEvent?.Invoke(data);
        }

        private static int buildId = 0;
        private static int tileId = 0;
        private static int troopId = 0;
        public int BuildId { get; }
        public int TroopId { get; }
        public int TileId { get; }

        public int IncrementBuildId() => buildId++;
        public int IncrementTroopId() => troopId++;
        public int IncrementTileId() => tileId++;
    }
}
