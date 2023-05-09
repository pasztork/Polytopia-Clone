using System.Text.Json;
using Util;

namespace Model
{
    public class MapManager : MapManagerBase
    {
        public override void GenerateMap()
        {
            GameManager.Get<MapGeneratorBase>().GenerateMap();
            SaveMap();
        }

        public override TileBase GetStartingTile()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            TileBase tile = StartingTiles[rand.Next(StartingTiles.Count)];
            StartingTiles.Remove(tile);
            return tile;
        }

        private void SaveMap()
        {
            string saveDirectory = $"{Directory.GetCurrentDirectory()}\\SavedMaps";
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            MapFilePath = Path.Combine(saveDirectory, $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.json");

            string jsonString = JsonSerializer.Serialize(
                new JsonTiles { Tiles = GetStringFormattedTiles() });
            File.WriteAllText(MapFilePath, jsonString);
        }

        private List<List<string>> GetStringFormattedTiles()
        {
            List<List<string>> result = new List<List<string>>();

            for (int row = 0; row < Tiles.GetLength(0); row++)
            {
                List<string> innerList = new List<string>();
                for (int column = 0; column < Tiles.GetLength(1); column++)
                {
                    innerList.Add(Tiles[row, column].ToString());
                }
                result.Add(innerList);
            }

            return result;
        }

        public override void LoadMap(string filePath)
        {
            Console.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            MapFilePath = filePath;

            string jsonString = File.ReadAllText(filePath);
            JsonTiles jsonTiles = JsonSerializer.Deserialize<JsonTiles>(jsonString);
            ConvertJsonToModel(jsonTiles);
            GameManager.Get<MapGeneratorBase>().ConnectLoadedMap();
        }

        private void ConvertJsonToModel(JsonTiles jsonTiles)
        {
            Factory<TileBase> factory = new Factory<TileBase>();
            IDictionary<string, Func<TileBase>> dict = new Dictionary<string, Func<TileBase>>
            {
                { "Forest", factory.Create<ForestTile> },
                { "Grass", factory.Create<GrassTile> },
                { "Rock", factory.Create<RockTile> },
                { "Sand", factory.Create<SandTile> },
                { "Water", factory.Create<WaterTile> }
            };
            Tiles = new TileBase[jsonTiles.Tiles.Count, jsonTiles.Tiles[0].Count];
            for (int row = 0; row < jsonTiles.Tiles.Count; row++)
            {
                for (int column = 0; column < jsonTiles.Tiles[0].Count; column++)
                {
                    Tiles[row, column] = dict[jsonTiles.Tiles[row][column]].Invoke();
                }
            }
            FindStartingTiles(jsonTiles);
        }

        private void FindStartingTiles(JsonTiles jsonTiles)
        {
            var size = jsonTiles.Tiles.Count;
            var offsets = new[] { (0, 0), (0, 1), (1, 0), (1, 1) };
            var rand = new Random(DateTime.Now.Millisecond);

            foreach ((int, int) offset in offsets)
            {
                var contenders = new List<TileBase>();
                for (int x = offset.Item1 * size / 2; x < (offset.Item1 + 1) * size / 2; x++)
                {
                    for (int y = offset.Item2 * size / 2; y < (offset.Item2 + 1) * size / 2; y++)
                    {
                        var tileName = jsonTiles.Tiles[x][y];
                        if (tileName.Equals("Grass") || tileName.Equals("Sand"))
                        {
                            contenders.Add(Tiles[x, y]);
                        }
                    }
                }
                GameManager.Get<MapManagerBase>().StartingTiles
                    .Add(contenders[rand.Next(contenders.Count)]);
            }
        }

        private class JsonTiles
        {
            public List<List<string>> Tiles { get; set; }
        }
    }
}