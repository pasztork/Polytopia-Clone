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

        public event Action? OnLoggingEnded;

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
                Name = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogAction.Build.ToString(),
                Parameters = new JsonLog.JsonActionParameters()
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
                Name = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogAction.Train.ToString(),
                Parameters = new JsonLog.JsonActionParameters()
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
                Name = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogAction.Move.ToString(),
                Parameters = new JsonLog.JsonActionParameters()
                {
                    Start = JsonLogger.GetTileCoords(from),
                    End = JsonLogger.GetTileCoords(target)
                }
            };
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerAttackTroop(Model.TroopBase attacker, Model.TileBase targetTile, List<Model.TileBase> targetedTiles)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Name = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogAction.AttackTroop.ToString(),
                Parameters = new JsonLog.JsonActionParameters()
                {
                    Start = JsonLogger.GetTileCoords(attacker.Tile),
                    End = JsonLogger.GetTileCoords(targetTile),
                    Troop = attacker.ToString(),
                    Neighbors = new int[targetedTiles.Count * 2]
                }
            };
            int idx = 0;
            foreach (var tile in targetedTiles)
            {
                action.Parameters.Neighbors[idx] = JsonLogger.GetTileCoords(tile)[0];
                action.Parameters.Neighbors[idx + 1] = JsonLogger.GetTileCoords(tile)[1];
                idx += 2;
            }
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerAttackBuilding(Model.TroopBase attacker, Model.TileBase targetTile, List<Model.TileBase> targetedTiles)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Name = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogAction.AttackBuilding.ToString(),
                Parameters = new JsonLog.JsonActionParameters()
                {
                    Start = JsonLogger.GetTileCoords(attacker.Tile),
                    End = JsonLogger.GetTileCoords(targetTile),
                    Troop = attacker.ToString()
                }
            };
            if (targetedTiles.Count > 0)
            {
                action.Parameters.Neighbors = new int[targetedTiles.Count * 2];
                int idx = 0;
                foreach (var tile in targetedTiles)
                {
                    action.Parameters.Neighbors[idx] = JsonLogger.GetTileCoords(tile)[0];
                    action.Parameters.Neighbors[idx + 1] = JsonLogger.GetTileCoords(tile)[1];
                    idx += 2;
                }
            }
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerTurnEnded()
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Name = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogAction.EndTurn.ToString(),
                Parameters = new JsonLog.JsonActionParameters()
            };
            JsonLogger.LogNewEvent(action);
        }

        public void TriggerGameEnded(Model.Player player)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Name = player.Name,
                Action = LogAction.EndGame.ToString(),
                Parameters = new JsonLog.JsonActionParameters()
            };
            JsonLogger.LogNewEvent(action);
            OnLoggingEnded?.Invoke();
        }

        public void TriggerTechLearned(Model.TechTreeItemBase tech)
        {
            JsonLog.JsonActionObject action = new JsonLog.JsonActionObject()
            {
                Name = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogAction.Learn.ToString(),
                Parameters = new JsonLog.JsonActionParameters()
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
                Name = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name,
                Action = LogAction.MissAttack.ToString(),
                Parameters = new JsonLog.JsonActionParameters()
                {
                    Start = JsonLogger.GetTileCoords(attacker.Tile),
                    End = JsonLogger.GetTileCoords(target.Tile)
                }
            };
            JsonLogger.LogNewEvent(action);
        }
    }
}