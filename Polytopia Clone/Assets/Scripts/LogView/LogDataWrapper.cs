using System;
using System.Collections.Generic;

namespace LogView
{
    public class LogDataWrapper
    {
        public event Action<JsonActionObject> NewDataCreated;

        private static LogDataWrapper instance;
        public static LogDataWrapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogDataWrapper();
                }
                return instance;
            }
        }

        public void SubscribeToPlayerEvents()
        {
            Model.GameManager.Get<Model.TurnManagerBase>().OnWinnerDecided += TriggerGameEnded;
            foreach (Model.Player player in Model.GameManager.Players)
            {
                player.BuildCreated += TriggerBuild;
                player.TroopDeath += TriggerTroopDeath;
                player.TroopTrained += TriggerTrain;
                player.TroopMoved += TriggerTroopMoved;
                player.TroopAttacked += TriggerAttack;
                player.TurnEnded += TriggerTurnEnded;
                player.BuildDestroyed += TriggerBuildingDestroy;
            }
        }

        public void TriggerBuild(Model.BuildingBase building)
        {
            JsonActionObject action = new JsonActionObject()
            {
                Action = LogActions.Build.ToString(),
                ActionDatas = new JsonActionDatas() { Building = building.ToString(),
                                                      Start = JsonLogger.GetTileCoords(building.Tile) }
            };
            NewDataCreated?.Invoke(action);
        }

        //kell ez az event actualy?? Catapultnal nem tom a szomszedos mezokon tortent dolgokat lekerni
        public void TriggerTroopDeath(Model.TroopBase troop)
        {
            //JsonDataHolder datas = new JsonDataHolder()
            //{
            //    Player = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
            //    Action = LogActions.Destroy.ToString(),
            //    //Tiles = new System.Collections.Generic.List<Identity>
            //    //{
            //    //     new Identity(){ Name = troop.Tile.ToString(),
            //    //                     Id = LogManager.Instance.IncrementTileId()}
            //    //},
            //    Troops = new List<Identity>
            //    {
            //        new Identity()
            //        {
            //            Name = troop.ToString(),
            //            Id = TroopToIdDic[troop]
            //        }
            //    }
            //};
            //NewDataCreated?.Invoke(datas);
        }

        //ugyanaz mint az elobb
        public void TriggerBuildingDestroy(Model.BuildingBase building)
        {
            //JsonDataHolder datas = new JsonDataHolder()
            //{
            //    Player = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
            //    Action = LogActions.Destroy.ToString(),
            //    //Tiles = new System.Collections.Generic.List<Identity>
            //    //{
            //    //     new Identity(){ Name = troop.Tile.ToString(),
            //    //                     Id = LogManager.Instance.IncrementTileId()}
            //    //},
            //    Buildings = new List<Identity>
            //    {
            //        new Identity()
            //        {
            //            Name = building.ToString(),
            //            Id = BuildingToIdDic[building]
            //        }
            //    }
            //};
            //NewDataCreated?.Invoke(datas);
        }

        public void TriggerTrain(Model.TroopBase troop)
        {
            JsonActionObject action = new JsonActionObject()
            {
                Action = LogActions.Train.ToString(),
                ActionDatas = new JsonActionDatas()
                {
                    Troop = troop.ToString(),
                    Start = JsonLogger.GetTileCoords(troop.Tile)
                }
            };
            NewDataCreated?.Invoke(action);
        }

        public void TriggerTroopMoved(Model.TroopBase troop, Model.TileBase from, Model.TileBase target)
        {
            JsonActionObject action = new JsonActionObject()
            {
                Action = LogActions.Move.ToString(),
                ActionDatas = new JsonActionDatas()
                {
                    Troop = troop.ToString(),
                    Start = JsonLogger.GetTileCoords(from),
                    End = JsonLogger.GetTileCoords(target)
                }
            };
            NewDataCreated?.Invoke(action);
        }

        public void TriggerAttack(Model.TroopBase attacker, Model.BuildingBase targetBuilding, Model.TroopBase targetTroop, Model.TileBase targetedTile)
        {
            JsonActionDatas datas;
            if (targetBuilding != null)
            {
                datas = new JsonActionDatas()
                {
                    Building = targetBuilding.ToString(),
                    Start = JsonLogger.GetTileCoords(attacker.Tile),
                    End = JsonLogger.GetTileCoords(targetBuilding.Tile)
                };
            }
            else
            {
                datas = new JsonActionDatas()
                {
                    Troop = targetTroop.ToString(),
                    Start = JsonLogger.GetTileCoords(attacker.Tile),
                    End = JsonLogger.GetTileCoords(targetTroop.Tile)
                };
            }

            JsonActionObject action = new JsonActionObject()
            {
                Action = LogActions.Attack.ToString(),
                ActionDatas = datas
            };
            NewDataCreated?.Invoke(action);
        }

        public void TriggerTurnEnded()
        {
            JsonActionObject action = new JsonActionObject()
            {
                Action = LogActions.Endturn.ToString(),
                ActionDatas = new JsonActionDatas()
            };
            NewDataCreated?.Invoke(action);
        }

        public void TriggerGameEnded(Model.Player player)
        {
            JsonActionObject action = new JsonActionObject()
            {
                Action = LogActions.GameEnd.ToString(),
                ActionDatas = new JsonActionDatas()
            };
            NewDataCreated?.Invoke(action);
        }
    }
}
