using System;
using System.Collections.Generic;

namespace View
{
    //Tile os id-k meg nem jok, elmentésnél az its ide elkene menteni
    public class LogDataWrapper
    {
        public event Action<JsonDataHolder> NewDataCreated;

        private Dictionary<int, Model.TroopBase> IdToTroopDic = new Dictionary<int, Model.TroopBase>();
        private Dictionary<Model.TroopBase, int> TroopToIdDic = new Dictionary<Model.TroopBase, int>();
        private Dictionary<int, Model.BuildingBase> IdToBildingDic = new Dictionary<int, Model.BuildingBase>();
        private Dictionary<Model.BuildingBase, int> BuildingToIdDic = new Dictionary<Model.BuildingBase, int>();
        private Dictionary<TileIdentity, Model.TileBase> IdToTileDic = new Dictionary<TileIdentity, Model.TileBase>();
        private Dictionary<Model.TileBase, TileIdentity> TileToIdDic = new Dictionary<Model.TileBase, TileIdentity>();

        private static LogDataWrapper instance;
        public static LogDataWrapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogDataWrapper();
                    LogDataWrapper.Instance.SubscribeToMapEvents();
                }
                return instance;
            }
        }

        private void SubscribeToMapEvents()
        {
            Model.GameManager.Get<Model.MapGeneratorBase>().TileGenerated += TriggerTileCreated;
        }

        public void SubscribeToPlayerEvents()
        {
            foreach (Model.Player player in Model.GameManager.Players)
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

        public void TriggerTileCreated(Model.TileBase tile, int x, int y)
        {
            TileIdentity newTileId = new TileIdentity() { X = x, Y = y, Name = tile.ToString() };
            TileToIdDic.Add(tile, newTileId);
            IdToTileDic.Add(newTileId, tile);

            JsonDataHolder datas = new JsonDataHolder()
            {
                Action = LogActions.TileCreation.ToString(),
                Tiles = new System.Collections.Generic.List<TileIdentity> { newTileId }
            };
            NewDataCreated?.Invoke(datas);
        }

        public void TriggerBuild(Model.BuildingBase building)
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
                Player = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer == null ? building.Player.Name : Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Build.ToString(),
                Buildings = new List<Identity> { build },
                //Tiles = new System.Collections.Generic.List<Identity> 
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId() }
                //}
            };
            NewDataCreated?.Invoke(datas);
        }

        public void TriggerTroopDeath(Model.TroopBase troop)
        {
            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Destroy.ToString(),
                //Tiles = new System.Collections.Generic.List<Identity>
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId()}
                //},
                Troops = new List<Identity>
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

        public void TriggerBuildingDestroy(Model.BuildingBase building)
        {
            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Destroy.ToString(),
                //Tiles = new System.Collections.Generic.List<Identity>
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId()}
                //},
                Buildings = new List<Identity>
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

        public void TriggerTrain(Model.TroopBase troop)
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
                Player = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Train.ToString(),
                //Tiles = new System.Collections.Generic.List<Identity>
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId() }
                //},
                Troops = new List<Identity> { troopData }
            };
            NewDataCreated?.Invoke(datas);
        }

        public void TriggerTroopMoved(Model.TroopBase troop, Model.TileBase from, Model.TileBase target)
        {
            Identity troopData = new Identity()
            {
                Name = troop.ToString(),
                Id = TroopToIdDic[troop]
            };

            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Move.ToString(),
                //Tiles = new System.Collections.Generic.List<Identity>
                //{
                //     new Identity(){ Name = troop.Tile.ToString(),
                //                     Id = LogManager.Instance.IncrementTileId()}
                //},
                Troops = new List<Identity> { troopData }
            };

            NewDataCreated?.Invoke(datas);
        }

        public void TriggerAttacked(Model.TroopBase attacker, Model.BuildingBase targetBuilding, Model.TroopBase targetTroop, Model.TileBase targetedTile)
        {
            List<Identity> troopData = new List<Identity>() { new Identity() { Id = TroopToIdDic[attacker], Name = attacker.ToString() } };

            if (targetTroop != null)
                troopData.Add(new Identity() { Id = TroopToIdDic[targetTroop], Name = targetTroop.ToString() });

            List<Identity> buildData = new List<Identity>();
            if (targetBuilding != null)
                buildData.Add(new Identity() { Name = targetBuilding.ToString(), Id = BuildingToIdDic[targetBuilding] });

            JsonDataHolder datas = new JsonDataHolder()
            {
                Player = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
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
                Player = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogActions.Endturn.ToString(),
            };
            NewDataCreated?.Invoke(datas);
        }
    }
}
