using Assets.Scripts.Logger.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    //Tile os id-k meg nem jok, elmentésnél az its ide elkene menteni
    public class LogDataWrapper
    {
        private Dictionary<int, TroopBase> IdToTroopDic = new Dictionary<int, TroopBase>();
        private Dictionary<TroopBase, int> TroopToIdDic = new Dictionary<TroopBase, int>();
        private Dictionary<int, BuildingBase> IdToBildingDic = new Dictionary<int, BuildingBase>();
        private Dictionary<BuildingBase, int> BuildingToIdDic = new Dictionary<BuildingBase, int>();
        private Dictionary<int, TileBase> IdToTileDic = new Dictionary<int, TileBase>();
        private Dictionary<TileBase, int> TileToIdDic = new Dictionary<TileBase, int>();

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
            Identity build = new Identity()
            {
                Name = building.ToString(),
                Id = LogManager.Instance.IncrementBuildId()
            };
            IdToBildingDic.Add(build.Id, building);
            BuildingToIdDic.Add(building, build.Id);

            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = TurnManager.Instance.CurrentPlayer.Name,
                Action = LogActions.Build,
                Buildings = new System.Collections.Generic.List<Identity> { build },
                //Tiles = new System.Collections.Generic.List<Identity> 
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId() }
                //}
            };

            LogManager.Instance.TriggerEvent(datas);
        }

        public void TriggerTroopDeath(TroopBase troop)
        {
            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = TurnManager.Instance.CurrentPlayer.Name,
                Action = LogActions.Destroy,
                //Tiles = new System.Collections.Generic.List<Identity>
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId()}
                //},
                Troops = new System.Collections.Generic.List<Identity>
                {
                    new Identity()
                    {
                        Name = troop.ToString(),
                        Id = TroopToIdDic[troop]
        } 
                }
            };

            LogManager.Instance.TriggerEvent(datas);
        }

        public void TriggerTrain(TroopBase troop)
        {
            Identity troopData = new Identity()
            {
                Name = troop.ToString(),
                Id = LogManager.Instance.IncrementTroopId()
            };
            IdToTroopDic.Add(troopData.Id, troop);
            TroopToIdDic.Add(troop, troopData.Id);

            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = TurnManager.Instance.CurrentPlayer.Name,
                Action = LogActions.Train,
                //Tiles = new System.Collections.Generic.List<Identity>
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId() }
                //},
                Troops = new System.Collections.Generic.List<Identity> { troopData }
            };

            LogManager.Instance.TriggerEvent(datas);
        }
    }
}
