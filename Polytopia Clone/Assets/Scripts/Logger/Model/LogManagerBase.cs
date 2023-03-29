using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logger.Model
{
    public abstract class LogManagerBase
    {
        public event Action<JsonDataHolder> LogEvent;
        public void TriggerEvent(JsonDataHolder data)
        {
            LogEvent?.Invoke(data);
        }

        private static int buildId = 0;
        private static int tileId = 0;
        private static int troopId = 0;
        public int BuildId { get => buildId;  set => buildId = value; }
        public int TroopId { get => troopId; set => troopId = value; }
        public int TileId { get => tileId; set => tileId = value; }

        public abstract int IncrementBuildId();
        public abstract int IncrementTroopId();
        public abstract int IncrementTileId(); 
    }
}
