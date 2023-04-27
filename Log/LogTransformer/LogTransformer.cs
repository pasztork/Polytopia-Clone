using JsonLog;
using System.Text.Json;

namespace LogView.LogTransformer
{
    public class LogTransformer
    {
        private readonly GameState _gameState = new();
        private string _json = string.Empty;

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
                if (!action.Name.Equals(player.Name)) { continue; }
                AddActionToPlayerState(action, result);
            }
            return result;
        }

        private void AddActionToPlayerState(JsonActionObject action, PlayerState playerState)
        {
            throw new NotImplementedException();
        }
    }
}
