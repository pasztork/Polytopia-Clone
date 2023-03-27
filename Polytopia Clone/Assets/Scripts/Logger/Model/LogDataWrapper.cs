using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LogDataWrapper
    {
        private static LogDataWrapper instance;
        public static LogDataWrapper Instance
        {
            get
            {
                instance ??= new LogDataWrapper();
                return instance;
            }
        }

        public void TriggerBuild(TroopBase troop, BuildingBase building)
        {
            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = TurnManager.Instance.CurrentPlayer.Name,
                Action = LogActions.Build,
                //Buildings = new System.Collections.Generic.List<BuildingBase> { building },
                //Tiles = new System.Collections.Generic.List<TileBase> { troop.Tile },
            };

            LogManager.Instance.TriggerEvent(datas);
        }

        public void TriggerTroopDeath(TroopBase troop)
        {
            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = TurnManager.Instance.CurrentPlayer.Name,
                Action = LogActions.Destroy,
                //Tiles = new System.Collections.Generic.List<TileBase> { troop.Tile },
                //Troops = new System.Collections.Generic.List<TroopBase> { troop }
            };

            LogManager.Instance.TriggerEvent(datas);
        }

        public void TriggerTrain(TroopBase troop)
        {
            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = TurnManager.Instance.CurrentPlayer.Name,
                Action = LogActions.Train,
                //Tiles = new System.Collections.Generic.List<TileBase> { troop.Tile },
                //Troops = new System.Collections.Generic.List<String> { troop.ToString() }
            };

            LogManager.Instance.TriggerEvent(datas);
        }
    }
}
