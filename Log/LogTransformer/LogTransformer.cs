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
                { "Attackbuilding", HandleAttackBuilding },
                { "Attacktroop", HandleAttackTroop },
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
        /// A string in JSON format containing the state of the game.
        /// </returns>
        public string Transform()
        {
            var filePath = JsonLogger.FilePath;
            var json = File.ReadAllText(filePath);
            var log = JsonSerializer.Deserialize<JsonDataHolder>(json);
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
            throw new NotImplementedException();
        }

        private void HandleAttackTroop(PlayerState playerState, JsonActionDatas actionDatas)
        {
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
            throw new NotImplementedException();
        }

        private void HandleMove(PlayerState playerState, JsonActionDatas actionDatas)
        {
            throw new NotImplementedException();
        }

        private void HandleTrain(PlayerState playerState, JsonActionDatas actionDatas)
        {
            throw new NotImplementedException();
        }
    }
}
