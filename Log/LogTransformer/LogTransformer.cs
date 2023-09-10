using JsonLog;
using Model;
using System.Text.Json;

namespace LogView.LogTransformer
{
    public class LogTransformer
    {
        public string LogFilePath { get; set; }
        public string SettingsFilePath { get; set; } = $"{Directory.GetCurrentDirectory()}\\GameSettings\\PropertiesSettings.json";

        private readonly GameState _gameState = new();

        private readonly Dictionary<string, Action<PlayerState, JsonActionParameters>> _processorFunctions;

        private readonly Dictionary<string, PlayerState> _playerStates = new();

        private Settings _settings = null;

        private readonly Dictionary<string, Dictionary<int[], int>> _playerBuildingHealths = new();
        private readonly Dictionary<string, Dictionary<int[], int>> _playerTroopHealths = new();
        private readonly Dictionary<string, Settings> _playerSettings = new();

        private string previousPlayer = "";

        public LogTransformer()
        {
            LogFilePath = JsonLogger.FilePath;
            _processorFunctions = new Dictionary<string, Action<PlayerState, JsonActionParameters>>
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
        public string TransformGameState()
        {
            string config = File.ReadAllText(SettingsFilePath);
            _settings = JsonSerializer.Deserialize<Settings>(config)!;
            var json = File.ReadAllText(LogFilePath);
            var log = JsonSerializer.Deserialize<JsonLogContent>(json) ??
                throw new JsonException("Unable to deserialize log file.");
            AssembleGameState(log);
            return JsonSerializer.Serialize(_gameState);
        }

        private void AssembleGameState(JsonLogContent log)
        {
            foreach (var player in log.Players)
            {
                var p = AssemblePlayerState(log, player);
                _playerStates.Add(p.Name, p);
            }

            foreach (var action in log.Actions)
            {
                if (!_processorFunctions.ContainsKey(action.Action))
                {
                    continue;
                }

                if (!previousPlayer.Equals(action.Name) && _playerStates[action.Name].Techs.Contains("Sanitation"))
                {
                    TechTransformer.Sanitation(_playerStates[action.Name], _playerTroopHealths[action.Name]);
                    previousPlayer = action.Name;
                }
                var function = _processorFunctions[action.Action];
                function.Invoke(_playerStates[action.Name], action.Parameters);
            }

            foreach (var playerState in _playerStates.Values)
            {
                _gameState.PlayerState.Add(playerState);
            }
        }

        private PlayerState AssemblePlayerState(JsonLogContent log, JsonPlayerObject player)
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

        private void HandleAttackBuilding(PlayerState playerState, JsonActionParameters Parameters)
        {
            var buildingOwner = GetPlayerWhoOwnsInList(Parameters.End, GetBuildingList);
            var buildingPosition =
                GetReferenceOfArrayWithSameValues(Parameters.End, _playerBuildingHealths[buildingOwner].Keys.ToList());
            _playerBuildingHealths[buildingOwner][buildingPosition]
                -= _playerSettings[playerState.Name].TroopProperties[Parameters.Troop].Damage;

            if (_playerBuildingHealths[buildingOwner][buildingPosition] <= 0)
            {
                var owner = _playerStates[buildingOwner];
                var allBuildings = new List<List<int[]>>
                {
                    owner.Banks,
                    owner.Cities,
                    owner.Farms,
                    owner.Harbors,
                    owner.Suppliers,
                };

                allBuildings.ForEach(b => FindAndRemoveIfPresent(buildingPosition, b));
                _playerBuildingHealths[buildingOwner].Remove(buildingPosition);
            }
        }

        private List<int[]> GetBuildingList(PlayerState playerState)
        {
            return playerState.Banks
                    .Concat(playerState.Cities)
                    .Concat(playerState.Farms)
                    .Concat(playerState.Harbors)
                    .Concat(playerState.Suppliers)
                    .ToList();
        }

        private void HandleAttackTroop(PlayerState playerState, JsonActionParameters Parameters)
        {
            var troopOwner = GetPlayerWhoOwnsInList(Parameters.End, GetTroopList);
            var troopPosition =
                GetReferenceOfArrayWithSameValues(Parameters.End, _playerTroopHealths[troopOwner].Keys.ToList());
            _playerTroopHealths[troopOwner][troopPosition]
                -= _playerSettings[playerState.Name].TroopProperties[Parameters.Troop].Damage;

            if (_playerTroopHealths[troopOwner][troopPosition] <= 0)
            {
                var owner = _playerStates[troopOwner];
                var allTroops = new List<List<int[]>>
                {
                    owner.Archers,
                    owner.Boats,
                    owner.Builders,
                    owner.Catapults,
                    owner.Scouts,
                    owner.Settlers,
                    owner.Warriors,
                };
                allTroops.ForEach(t => FindAndRemoveIfPresent(troopPosition, t));
                _playerTroopHealths[troopOwner].Remove(troopPosition);
            }
        }

        private List<int[]> GetTroopList(PlayerState playerState)
        {
            return playerState.Archers
                    .Concat(playerState.Boats)
                    .Concat(playerState.Builders)
                    .Concat(playerState.Catapults)
                    .Concat(playerState.Scouts)
                    .Concat(playerState.Settlers)
                    .Concat(playerState.Warriors)
                    .ToList();
        }

        private string GetPlayerWhoOwnsInList(int[] coord, Func<PlayerState, List<int[]>> listFunc)
        {
            foreach (var p in _playerStates.Values)
            {
                foreach (var t in listFunc.Invoke(p))
                {
                    if (coord[0] == t[0] && coord[1] == t[1])
                    {
                        return p.Name;
                    }
                }
            }

            return string.Empty;
        }

        private int[] GetReferenceOfArrayWithSameValues(int[] values, List<int[]> original)
        {
            foreach (var v in original)
            {
                if (values[0] == v[0] && values[1] == v[1])
                {
                    return v;
                }
            }

            return Array.Empty<int>();
        }

        private void HandleBuild(PlayerState playerState, JsonActionParameters Parameters)
        {
            var stringFieldMap = new Dictionary<string, List<int[]>>
            {
                { "Bank", playerState.Banks },
                { "City", playerState.Cities },
                { "Farm", playerState.Farms },
                { "Harbor", playerState.Harbors },
                { "Supplier", playerState.Suppliers },
            };
            stringFieldMap[Parameters.Building].Add(Parameters.Start);
            _playerBuildingHealths[playerState.Name].Add(Parameters.Start, _settings.BuildingProperties[Parameters.Building].Health);

            FindAndRemoveIfPresent(Parameters.Start, playerState.Builders);
            FindAndRemoveIfPresent(Parameters.Start, playerState.Settlers);
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

        private void HandleLearn(PlayerState playerState, JsonActionParameters Parameters)
        {
            playerState.Techs.Add(Parameters.Tech);

            if (Parameters.Tech.Equals("Militarism"))
            {
                TechTransformer.Militarism(_playerSettings[playerState.Name]);
            }
        }

        private void HandleMove(PlayerState playerState, JsonActionParameters Parameters)
        {
            List<int[]> moveableList = new();
            moveableList.AddRange(playerState.Archers);
            moveableList.AddRange(playerState.Boats);
            moveableList.AddRange(playerState.Builders);
            moveableList.AddRange(playerState.Catapults);
            moveableList.AddRange(playerState.Scouts);
            moveableList.AddRange(playerState.Settlers);
            moveableList.AddRange(playerState.Warriors);

            moveableList.ForEach(moveable => MoveIfNecessary(moveable, Parameters));
        }

        private void MoveIfNecessary(int[] moveable, JsonActionParameters Parameters)
        {
            if (moveable[0] == Parameters.Start[0] &&
                moveable[1] == Parameters.Start[1])
            {
                moveable[0] = Parameters.End[0];
                moveable[1] = Parameters.End[1];
            }
        }

        private void HandleTrain(PlayerState playerState, JsonActionParameters Parameters)
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
            stringTroopMap[Parameters.Troop].Add(Parameters.Start);
            _playerTroopHealths[playerState.Name].Add(Parameters.Start, _settings.TroopProperties[Parameters.Troop].Health);
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
