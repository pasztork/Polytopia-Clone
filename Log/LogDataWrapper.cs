using System.Collections.Generic;

namespace LogView
{
    public class LogDataWrapper
    {
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
            foreach (Model.Player player in Model.GameManager.Players)
            {
                player.OnBuildCreated += TriggerBuild;
                player.OnTroopTrained += TriggerTrain;
                player.OnTroopMoved += TriggerTroopMoved;
                player.OnTroopAttacked += TriggerAttackTroop;
                player.OnBuildingAttacked += TriggerAttackBuilding;
                player.OnTurnEnded += TriggerTurnEnded;
                player.OnTechLearned += TriggerTechLearned;
                player.OnAttackMissed += TriggerAttackMissed;
            }
            Model.GameManager.Get<Model.TurnManagerBase>().OnWinnerDecided += TriggerGameEnded;
        }

        public void TriggerBuild(Model.BuildingBase building)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Action = LogActions.Build.ToString(),
                ActionDatas = new JsonLog.JsonActionDatas()
                {
                    Building = building.ToString(),
                    Start = JsonLogger.GetTileCoords(building.Tile)
                }
            };
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerTrain(Model.TroopBase troop)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Action = LogActions.Train.ToString(),
                ActionDatas = new JsonLog.JsonActionDatas()
                {
                    Troop = troop.ToString(),
                    Start = JsonLogger.GetTileCoords(troop.Tile)
                }
            };
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerTroopMoved(Model.TileBase from, Model.TileBase target)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Action = LogActions.Move.ToString(),
                ActionDatas = new JsonLog.JsonActionDatas()
                {
                    Start = JsonLogger.GetTileCoords(from),
                    End = JsonLogger.GetTileCoords(target)
                }
            };
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerAttackTroop(Model.TileBase attackerTile, Model.TileBase targetTile, List<Model.TileBase> targetedTiles)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Action = LogActions.Attacktroop.ToString(),
                ActionDatas = new JsonLog.JsonActionDatas()
                {
                    Start = JsonLogger.GetTileCoords(attackerTile),
                    End = JsonLogger.GetTileCoords(targetTile),
                    Neighbors = new int[targetedTiles.Count * 2]
                }
            };
            int idx = 0;
            foreach (var tile in targetedTiles)
            {
                action.ActionDatas.Neighbors[idx] = JsonLogger.GetTileCoords(tile)[0];
                action.ActionDatas.Neighbors[idx + 1] = JsonLogger.GetTileCoords(tile)[1];
                idx += 2;
            }
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerAttackBuilding(Model.TileBase attackerTile, Model.TileBase targetTile, List<Model.TileBase> targetedTiles)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Action = LogActions.Attackbuilding.ToString(),
                ActionDatas = new JsonLog.JsonActionDatas()
                {
                    Start = JsonLogger.GetTileCoords(attackerTile),
                    End = JsonLogger.GetTileCoords(targetTile)
                }
            };
            if (targetedTiles.Count > 0)
            {
                action.ActionDatas.Neighbors = new int[targetedTiles.Count * 2];
                int idx = 0;
                foreach (var tile in targetedTiles)
                {
                    action.ActionDatas.Neighbors[idx] = JsonLogger.GetTileCoords(tile)[0];
                    action.ActionDatas.Neighbors[idx + 1] = JsonLogger.GetTileCoords(tile)[1];
                    idx += 2;
                }
            }
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerTurnEnded()
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Action = LogActions.Endturn.ToString(),
                ActionDatas = new JsonLog.JsonActionDatas()
            };
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerGameEnded(Model.Player player)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Action = LogActions.Gameend.ToString(),
                ActionDatas = new JsonLog.JsonActionDatas()
            };
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerTechLearned(Model.TechTreeItemBase tech)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Action = LogActions.Learn.ToString(),
                ActionDatas = new JsonLog.JsonActionDatas()
                {
                    Tech = tech.HashCode
                }
            };
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerAttackMissed(Model.TroopBase attacker, Model.TroopBase target)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Action = LogActions.Missattack.ToString(),
                ActionDatas = new JsonLog.JsonActionDatas()
                {
                    Start = JsonLogger.GetTileCoords(attacker.Tile),
                    End = JsonLogger.GetTileCoords(target.Tile)
                }
            };
            JsonLogger.LogNewEvent(action);
        }
    }
}