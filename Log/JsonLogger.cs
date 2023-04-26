using System.Text.Json;

namespace LogView
{
    public static class JsonLogger
    {
        private static readonly string saveDirectory = $"{Directory.GetCurrentDirectory()}\\GameLogs";
        private static readonly string filePath = $"{saveDirectory}\\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.json";
        public static string FilePath { get { return filePath; } }

        private static readonly Dictionary<Model.TileBase, int[]> tileToCoordMap = new Dictionary<Model.TileBase, int[]>();
        private static readonly JsonDataHolder log = new JsonDataHolder();

        public static void Init()
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            Model.GameManager.OnGameStarted += LogStart;
            MapTilesToCoords();
        }

        private static void LogStart(IList<Model.Player> players, string mapFilePath)
        {
            log.Map = mapFilePath;
            log.Players = new List<JsonPlayerObject>();
            foreach (Model.Player p in players)
            {
                log.Players.Add(new JsonPlayerObject
                {
                    Name = p.Name,
                    StartingTile = tileToCoordMap[p.Buildings[0].Tile],
                    StartingCityRange = p.StartingCityRange
                });
            }
            log.Actions = new List<JsonLog.JsonActionObject>();
            string jsonString = JsonSerializer.Serialize(log,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);

            LogDataWrapper.Instance.SubscribeToPlayerEvents();
        }

        public static void LogNewEvent(JsonLog.JsonActionObject newAction)
        {
            log.Actions.Add(newAction);

            string jsonString = JsonSerializer.Serialize(log, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }

        private static void MapTilesToCoords()
        {
            Model.TileBase[,] tiles = Model.GameManager.Get<Model.MapManagerBase>().Tiles;
            for (int row = 0; row < tiles.GetLength(0); row++)
            {
                for (int column = 0; column < tiles.GetLength(1); column++)
                {
                    tileToCoordMap.Add(tiles[row, column], new int[] { row, column });
                }
            }
        }

        public static int[] GetTileCoords(Model.TileBase tile)
        {
            return tileToCoordMap[tile];
        }
    }
}
