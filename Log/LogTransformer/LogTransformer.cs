using JsonLog;
using System.Text.Json;

namespace LogView.LogTransformer
{
    public class LogTransformer
    {
        private readonly GameState _gameState = new();

        private readonly Dictionary<string, Action<PlayerState, JsonActionDatas>> _processorFunctions;

        public LogTransformer()
        {
            _processorFunctions = new Dictionary<string, Action<PlayerState, JsonActionDatas>>
            {
                { "AttackBuilding", HandleAttackBuilding },
                { "AttackTroop", HandleAttackTroop },
                { "Build", HandleBuild },
                { "Learn", HandleLearn },
                { "Move", HandleMove },
                { "Train", HandleTrain },
            };
        }

        /// <summary>
        /// Transforms the log file belonging to the current game into
        /// one JSON object that represents the current inner state.
        /// </summary>
        /// <returns>
        /// A string in JSON format containing the current state of the game.
        /// </returns>
        public string Transform()
        {
            var filePath = JsonLogger.FilePath;
            var json = File.ReadAllText(filePath);
            var log = JsonSerializer.Deserialize<JsonDataHolder>(json) ??
                throw new JsonException("Unable to deserialize log file.");
            AssembleGameState(log);
            return JsonSerializer.Serialize(_gameState);
        }

        private void AssembleGameState(JsonDataHolder log)
        {
            foreach (var player in log.Players)
            {
                var p = AssemblePlayerState(log, player);
                _gameState.PlayerState.Add(p);
            }
        }

        private PlayerState AssemblePlayerState(JsonDataHolder log, JsonPlayerObject player)
        {
            var result = new PlayerState { Name = player.Name };
            result.Cities.Add(player.StartingTile);
            foreach (var action in log.Actions)
            {
                if (action.Name.Equals(player.Name) && _processorFunctions.ContainsKey(action.Action))
                {
                    var function = _processorFunctions[action.Action];
                    function.Invoke(result, action.ActionDatas);
                }
            }
            return result;
        }

        private void HandleAttackBuilding(PlayerState playerState, JsonActionDatas actionDatas)
        {
            // TODO:
            // Keep track of each players buildings.
            // One dictionary is mapped to each player.
            // Inside each dictionary coordinates are
            // mapped to their health values (int).
            // Decrease health of attacked building, 
            // based on the settings used.
            // Remove building from playerState if destroyed.
            throw new NotImplementedException();
        }

        private void HandleAttackTroop(PlayerState playerState, JsonActionDatas actionDatas)
        {
            // TODO:
            // Keep track of each players troops.
            // One dictionary is mapped to each player.
            // Inside each dictionary coordinates are
            // mapped to their health values (int).
            // Decrease health of attacked troop, 
            // based on the settings used.
            // Remove troop from playerState if destroyed.
            throw new NotImplementedException();
        }

        private void HandleBuild(PlayerState playerState, JsonActionDatas actionDatas)
        {
            var stringFieldMap = new Dictionary<string, List<int[]>>
            {
                { "Bank", playerState.Banks },
                { "City", playerState.Cities },
                { "Farm", playerState.Farms },
                { "Harbor", playerState.Harbors },
                { "Supplier", playerState.Suppliers },
            };
            stringFieldMap[actionDatas.Building].Add(actionDatas.Start);

            FindAndRemoveIfPresent(actionDatas.Start, playerState.Builders);
            FindAndRemoveIfPresent(actionDatas.Start, playerState.Settlers);
        }

        private void FindAndRemoveIfPresent(int[] posToFind, List<int[]> findHere)
        {
            var toRemove = Array.Empty<int>();
            foreach (var pos in findHere)
            {
                if (pos[0] == posToFind[0] && pos[1] == posToFind[1])
                {
                    toRemove = pos;
                    break;
                }
            }
            findHere.Remove(toRemove);
        }

        private void HandleLearn(PlayerState playerState, JsonActionDatas actionDatas)
        {
            // TODO:
            // Tech items might affect future actions.
            // Settings should be mapped to each player.
            // This way we can produce a precise copy of the game state.
            // Some players' troops might have more health etc.
            playerState.Techs.Add(actionDatas.Tech);
        }

        private void HandleMove(PlayerState playerState, JsonActionDatas actionDatas)
        {
            List<int[]> moveableList = new();
            moveableList.AddRange(playerState.Archers);
            moveableList.AddRange(playerState.Boats);
            moveableList.AddRange(playerState.Builders);
            moveableList.AddRange(playerState.Catapults);
            moveableList.AddRange(playerState.Scouts);
            moveableList.AddRange(playerState.Settlers);
            moveableList.AddRange(playerState.Warriors);

            moveableList.ForEach(moveable => MoveIfNecessary(moveable, actionDatas));
        }

        private void MoveIfNecessary(int[] moveable, JsonActionDatas actionDatas)
        {
            if (moveable[0] == actionDatas.Start[0] &&
                moveable[1] == actionDatas.Start[1])
            {
                moveable[0] = actionDatas.End[0];
                moveable[1] = actionDatas.End[1];
            }
        }

        private void HandleTrain(PlayerState playerState, JsonActionDatas actionDatas)
        {
            var stringTroopMap = new Dictionary<string, List<int[]>>
            {
                { "Archer", playerState.Archers },
                { "Boat", playerState.Boats },
                { "Builder", playerState.Builders },
                { "Catapult", playerState.Catapults },
                { "Scout", playerState.Scouts },
                { "Settler", playerState.Settlers },
                { "Warrior", playerState.Warriors },
            };
            stringTroopMap[actionDatas.Building].Add(actionDatas.Start);
        }
    }
}
