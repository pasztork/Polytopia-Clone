using Assets.Scripts.Logger.Model;
using System;
using System.Collections.Generic;

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

        public event Action<JsonDataHolder> NewDataCreated;

        private static LogDataWrapper instance;
        public static LogDataWrapper Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new LogDataWrapper();
                    LogDataWrapper.Instance.SubscribeToEvents();
                }
                return instance;
            }
        }

        private void SubscribeToEvents()
        {
            foreach(var player in DependencyContainer.Get<GameManager>().Players)
            {
                player.BuildCreated += TriggerBuild;
                player.TroopDeath += TriggerTroopDeath;
                player.TroopTrained += TriggerTrain;
                player.TroopMoved += TriggerTroopMoved;
                player.TroopAttacked += TriggerAttacked;
                player.TurnEnded += TriggerTurnEnded;
                player.BuildDestroyed += TriggerBuildingDestroy;
            }
        }

        public void TriggerBuild(BuildingBase building)
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
                Player = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Build.ToString(),
                Buildings = new System.Collections.Generic.List<Identity> { build },
                //Tiles = new System.Collections.Generic.List<Identity> 
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId() }
                //}
            };

            NewDataCreated?.Invoke(datas);
        }

        public void TriggerTroopDeath(TroopBase troop)
        {
            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Destroy.ToString(),
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

            NewDataCreated?.Invoke(datas);
        }

        public void TriggerBuildingDestroy(BuildingBase building)
        {
            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Destroy.ToString(),
                //Tiles = new System.Collections.Generic.List<Identity>
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId()}
                //},
                Buildings = new System.Collections.Generic.List<Identity>
                {
                    new Identity()
                    {
                        Name = building.ToString(),
                        Id = BuildingToIdDic[building]
                    }
                }
            };

            NewDataCreated?.Invoke(datas);
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
                Player = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Train.ToString(),
                //Tiles = new System.Collections.Generic.List<Identity>
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId() }
                //},
                Troops = new System.Collections.Generic.List<Identity> { troopData }
            };

            NewDataCreated?.Invoke(datas);
        }

        public void TriggerTroopMoved(TroopBase troop, TileBase from, TileBase target)
        {
            Identity troopData = new Identity()
            {
                Name = troop.ToString(),
                Id = TroopToIdDic[troop]
            };

            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Move.ToString(),
                //Tiles = new System.Collections.Generic.List<Identity>
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId()}
                //},
                Troops = new System.Collections.Generic.List<Identity>{ troopData }
            };

            NewDataCreated?.Invoke(datas);
        }

        public void TriggerAttacked(TroopBase attacker, BuildingBase targetBuilding, TroopBase targetTroop, TileBase targetedTile)
        {
            List<Identity> troopData = new List<Identity>() { new Identity() { Id = TroopToIdDic[attacker], Name = attacker.ToString() } };

            if(targetTroop != null)
                troopData.Add(new Identity() { Id = TroopToIdDic[targetTroop], Name = targetTroop.ToString() });

            List<Identity> buildData = new List<Identity>();
            if(targetBuilding != null)
                buildData.Add(new Identity() {  Name = targetBuilding.ToString(), Id = BuildingToIdDic[targetBuilding] });

            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Attack.ToString(),
                //Tiles = new System.Collections.Generic.List<Identity>
                //{
                //     new Identity(){ Name = targetedTile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId()}
                //},
                Troops = troopData,
                Buildings = buildData
            };
            NewDataCreated?.Invoke(datas);
        }

        public void TriggerTurnEnded()
        {
            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Endturn.ToString(),
            };
            NewDataCreated?.Invoke(datas);
        }
    }
}
