using System;
using System.Collections.Generic;
using System.IO;
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

            MapFilePath = Path.Combine(saveDirectory, $"{DateTime.Now:yyyy-mm-dd_hh-mm-ss}.json");

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
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            MapFilePath = filePath;

            string jsonString = File.ReadAllText(filePath);
            JsonTiles jsonTiles = JsonSerializer.Deserialize<JsonTiles>(jsonString);
            ConvertJsonToModel(jsonTiles);
        }

        private void ConvertJsonToModel(JsonTiles jsonTiles)
        {
            // Create factory to create tiles that are cast back to TileBase
            Factory<TileBase> factory = new Factory<TileBase>();
            IDictionary<string, Func<TileBase>> dict = new Dictionary<string, Func<TileBase>>
            {
                { "Forest", factory.Create<ForestTile> },
                { "Grass", factory.Create<GrassTile> },
                { "Rock", factory.Create<RockTile> },
                { "Sand", factory.Create<SandTile> },
                { "Water", factory.Create<WaterTile> }
            };

            for (int row = 0; row < Tiles.GetLength(0); row++)
            {
                for (int column = 0; column < Tiles.GetLength(1); column++)
                {
                    Tiles[row, column] = dict[jsonTiles.Tiles[row][column]].Invoke();
                }
            }
        }

        internal class JsonTiles
        {
            public List<List<string>> Tiles { get; set; }
        }
    }
}