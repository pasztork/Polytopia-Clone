using JsonLog;
using Model;
using System.Text.Json;

namespace LogView.LogTransformer
{
    public class LogTransformer
    {
        private readonly GameState _gameState = new();

        private readonly Dictionary<string, Action<PlayerState, JsonActionDatas>> _processorFunctions;

        private readonly Dictionary<string, PlayerState> _playerStates = new();

        private static readonly string directory = $"{Directory.GetCurrentDirectory()}\\GameSettings";
        private static readonly string propertiesSettingsFilename = "PropertiesSettings.json";

        private readonly Settings _settings;

        private readonly Dictionary<string, Dictionary<int[], int>> _playerBuildingHealths = new();
        private readonly Dictionary<string, Dictionary<int[], int>> _playerTroopHealths = new();
        private readonly Dictionary<string, Settings> _playerSettings = new();

        private string previousPlayer = "";

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
            string config = File.ReadAllText(Path.Combine(directory, propertiesSettingsFilename));
            _settings = JsonSerializer.Deserialize<Settings>(config)!;
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
                _playerStates.Add(p.Name, p);
            }
            foreach (var action in log.Actions)
            {
                if (!previousPlayer.Equals(action.Name) && _playerStates[action.Name].Techs.Contains("Sanitation"))
                {
                    TechTransformer.Sanitation(_playerStates[action.Name], _playerTroopHealths[action.Name]);
                    previousPlayer = action.Name;
                }
                var function = _processorFunctions[action.Action];
                function.Invoke(_playerStates[action.Name], action.ActionDatas);
            }
            foreach (var playerState in _playerStates.Values)
            {
                _gameState.PlayerState.Add(playerState);
            }
        }

        private PlayerState AssemblePlayerState(JsonDataHolder log, JsonPlayerObject player)
        {
            var result = new PlayerState { Name = player.Name };
            _playerBuildingHealths.Add(result.Name, new Dictionary<int[], int>());
            _playerTroopHealths.Add(result.Name, new Dictionary<int[], int>());
            _playerSettings.Add(result.Name, new Settings()
            {
                BaseProduction = _settings.BaseProduction,
                BuildingProperties = _settings.BuildingProperties,
                TechTreeItemCosts = _settings.TechTreeItemCosts,
                TroopProperties = _settings.TroopProperties,
            }
            );
            result.Cities.Add(player.StartingTile);
            return result;
        }

        private void HandleAttackBuilding(PlayerState playerState, JsonActionDatas actionDatas)
        {
            _playerBuildingHealths[playerState.Name][actionDatas.End]
                -= _playerSettings[playerState.Name].TroopProperties[actionDatas.Troop].Damage;

            if (_playerBuildingHealths[playerState.Name][actionDatas.End] <= 0)
            {
                List<int[]> buildingsList =
                    playerState.Banks
                    .Concat(playerState.Cities)
                    .Concat(playerState.Farms)
                    .Concat(playerState.Harbors)
                    .Concat(playerState.Suppliers)
                    .ToList();

                buildingsList.Remove(actionDatas.End);
                _playerBuildingHealths[playerState.Name].Remove(actionDatas.End);
            }
        }

        private void HandleAttackTroop(PlayerState playerState, JsonActionDatas actionDatas)
        {
            _playerTroopHealths[playerState.Name][actionDatas.End]
                -= _playerSettings[playerState.Name].TroopProperties[actionDatas.Troop].Damage;

            if (_playerTroopHealths[playerState.Name][actionDatas.End] <= 0)
            {
                List<int[]> troopsList =
                    playerState.Archers
                    .Concat(playerState.Boats)
                    .Concat(playerState.Builders)
                    .Concat(playerState.Catapults)
                    .Concat(playerState.Scouts)
                    .Concat(playerState.Settlers)
                    .Concat(playerState.Warriors)
                    .ToList();

                troopsList.Remove(actionDatas.End);
                _playerTroopHealths[playerState.Name].Remove(actionDatas.End);
            }
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
            _playerBuildingHealths[playerState.Name].Add(actionDatas.Start, _settings.BuildingProperties[actionDatas.Building].Health);

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
            playerState.Techs.Add(actionDatas.Tech);

            if (actionDatas.Tech.Equals("Militarism"))
            {
                TechTransformer.Militarism(_playerSettings[playerState.Name]);
            }
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

            moveableList.ForEach(moveable => MoveIfNecessary(playerState, moveable, actionDatas));
        }

        private void MoveIfNecessary(PlayerState playerState, int[] moveable, JsonActionDatas actionDatas)
        {
            if (moveable[0] == actionDatas.Start[0] &&
                moveable[1] == actionDatas.Start[1])
            {
                moveable[0] = actionDatas.End[0];
                moveable[1] = actionDatas.End[1];
                int health = _playerTroopHealths[playerState.Name][actionDatas.Start];
                _playerTroopHealths[playerState.Name].Remove(actionDatas.Start);
                _playerTroopHealths[playerState.Name].Add(actionDatas.End, health);
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
            stringTroopMap[actionDatas.Troop].Add(actionDatas.Start);
            _playerTroopHealths[playerState.Name].Add(actionDatas.Start, _settings.TroopProperties[actionDatas.Troop].Health);
        }
    }


    public static class TechTransformer
    {

        public static void Militarism(Settings settings)
        {
            settings.TroopProperties["Archer"].Damage += 1;
            settings.TroopProperties["Boat"].Damage += 1;
            settings.TroopProperties["Catapult"].MovementRange += 1;
            settings.TroopProperties["Scout"].MovementRange += 1;
            settings.TroopProperties["Warrior"].MovementRange += 1;
        }

        public static void Sanitation(PlayerState playerState, Dictionary<int[], int> troopHealths)
        {
            HashSet<int[]> tilesInCityRange = new HashSet<int[]>();
            foreach (int[] coords in playerState.Cities)
            {
                tilesInCityRange.Add(coords);
                tilesInCityRange.Add(new int[] { coords[0] - 1, coords[1] - 1 });
                tilesInCityRange.Add(new int[] { coords[0] - 1, coords[1] });
                tilesInCityRange.Add(new int[] { coords[0] - 1, coords[1] + 1 });
                tilesInCityRange.Add(new int[] { coords[0], coords[1] - 1 });
                tilesInCityRange.Add(new int[] { coords[0], coords[1] + 1 });
                tilesInCityRange.Add(new int[] { coords[0] + 1, coords[1] - 1 });
                tilesInCityRange.Add(new int[] { coords[0] + 1, coords[1] });
                tilesInCityRange.Add(new int[] { coords[0] + 1, coords[1] + 1 });
            }
            List<int[]> troopsList =
                    playerState.Archers
                    .Concat(playerState.Boats)
                    .Concat(playerState.Builders)
                    .Concat(playerState.Catapults)
                    .Concat(playerState.Scouts)
                    .Concat(playerState.Settlers)
                    .Concat(playerState.Warriors)
                    .ToList();

            foreach (int[] troopCoords in troopsList)
            {
                if (tilesInCityRange.Contains(troopCoords))
                {
                    troopHealths[troopCoords] += 1;
                }
            }
        }
    }
}
