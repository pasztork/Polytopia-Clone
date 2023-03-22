using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class MapGenerator
    {
        public static MapGenerator instance;
        public static MapGenerator Instance
        {
            get
            {
                instance ??= new MapGenerator();
                return instance;
            }
        }

        private TileBase[,] tiles;
        private float[,] noiseMap;
        private int size;

        public int MinMountainCount { private get; set; }
        public int MaxMountainCount { private get; set; }
        public int MinForrestCountPerChunk { private get; set; }
        public int MaxForrestCountPerChunk { private get; set; }
        public float WaterTileProbability { private get; set; }
        public float DesertChunkProbability { private get; set; }

        public void GenerateMap()
        {

            tiles = MapManager.Instance.Tiles;
            size = MapManager.Instance.Tiles.GetLength(0);
            noiseMap = PerlinNoise.GenerateNoiseMap(size);
            GenerateWaterTiles();
            FillEmptyTiles();
            SetupCoordinateSystem();
        }

        private void GenerateWaterTiles()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    tiles[x, y] = noiseMap[x, y] < WaterTileProbability
                        ? new WaterTile()
                        : null;
        }

        private void FillEmptyTiles()
        {
            GenerateMountains();

            (int, int)[] offsets = { (0, 0), (0, 1), (1, 0), (1, 1) };
            foreach ((int, int) offset in offsets)
                GenerateChunk(offset);
        }

        private void GenerateMountains()
        {
            int actualMountainCount = new System.Random().Next(MaxMountainCount - MinMountainCount + 1) + MinMountainCount;
            if (actualMountainCount == 0)
                return;

            IList<(int, int)> emptyCoords = new List<(int, int)>();
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (tiles[x, y] == null)
                        emptyCoords.Add((x, y));

            IList<(int, int)> mountainCoords = new List<(int, int)>();
            System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
            for (int i = 0; i < actualMountainCount; i++)
            {
                (int, int) pair = emptyCoords[rand.Next(emptyCoords.Count)];
                emptyCoords.Remove(pair);
                mountainCoords.Add(pair);
            }

            foreach ((int, int) mountainCoord in mountainCoords)
                tiles[mountainCoord.Item1, mountainCoord.Item2] = new RockTile();
        }

        private void GenerateChunk((int, int) offset)
        {
            if (new System.Random().NextDouble() < DesertChunkProbability)
            {
                GenerateDesert(offset);
                return;
            }

            GenerateGrassLand(offset);
        }

        private void GenerateDesert((int, int) offset)
        {
            for (int x = offset.Item1 * size / 2; x < (offset.Item1 + 1) * size / 2; x++)
                for (int y = offset.Item2 * size / 2; y < (offset.Item2 + 1) * size / 2; y++)
                    tiles[x, y] ??= new SandTile();
        }

        private void GenerateGrassLand((int, int) offset)
        {
            IList<(int, int, float)> emptyCoords = new List<(int, int, float)>();
            for (int x = offset.Item1 * size / 2; x < (offset.Item1 + 1) * size / 2; x++)
                for (int y = offset.Item2 * size / 2; y < (offset.Item2 + 1) * size / 2; y++)
                    if (tiles[x, y] == null)
                        emptyCoords.Add((x, y, noiseMap[x, y]));

            int actualForrestCount = new System.Random().Next(
                MaxForrestCountPerChunk - MinForrestCountPerChunk + 1) + MinForrestCountPerChunk;
            foreach ((int, int) forrestCoord in
                emptyCoords.OrderBy(x => x.Item3).TakeLast(System.Math.Min(emptyCoords.Count, actualForrestCount)).Select(x => (x.Item1, x.Item2)))
                tiles[forrestCoord.Item1, forrestCoord.Item2] = new ForrestTile();

            foreach ((int, int) coord in emptyCoords.Select(x => (x.Item1, x.Item2)))
                tiles[coord.Item1, coord.Item2] ??= new GrassTile();
        }

        private void SetupCoordinateSystem()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    AddNeighborsToList(x, y);
        }

        private void AddNeighborsToList(int x, int y)
        {
            TileBase tile = tiles[x, y];
            (int, int)[] neighborCoordinates = {
            (x - 1, y - 1), (x - 1, y), (x - 1, y + 1),
            (x, y - 1),                 (x, y + 1),
            (x + 1, y - 1), (x + 1, y), (x + 1, y + 1)
        };
            foreach ((int, int) coordinate in neighborCoordinates)
                if (IsValidCoordinate(coordinate))
                    tile.Neighbors.Add(tiles[coordinate.Item1, coordinate.Item2]);
        }

        private bool IsValidCoordinate((int, int) coordinate)
        {
            return
                coordinate.Item1 >= 0 &&
                coordinate.Item2 >= 0 &&
                coordinate.Item1 < size &&
                coordinate.Item2 < size;
        }
    }
}