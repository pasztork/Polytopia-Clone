namespace Model
{
    public class MapGenerator : MapGeneratorBase
    {
        private TileBase[,] tiles;
        private float[,] noiseMap;

        public override void GenerateMap()
        {
            GameManager.Get<MapManagerBase>().Tiles = new TileBase[MGP.Size, MGP.Size];
            tiles = GameManager.Get<MapManagerBase>().Tiles;
            noiseMap = NoiseFunction.Invoke(MGP.Size);
            GenerateWaterTiles();
            FillEmptyTiles();
            SetupCoordinateSystem();
        }

        public override void ConnectLoadedMap()
        {
            tiles = GameManager.Get<MapManagerBase>().Tiles;
            MGP.Size = tiles.GetLength(0);
            SetupCoordinateSystem();
        }

        private void GenerateWaterTiles()
        {
            for (int x = 0; x < MGP.Size; x++)
            {
                for (int y = 0; y < MGP.Size; y++)
                {
                    if (noiseMap[x, y] < MGP.WaterTileProbability)
                    {
                        tiles[x, y] = new WaterTile();
                    }
                    else
                    {
                        tiles[x, y] = null;
                    }
                }
            }
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
            int actualMountainCount = new System.Random(System.DateTime.Now.Millisecond).Next(
                MGP.MaxMountainCount - MGP.MinMountainCount + 1)
                + MGP.MinMountainCount;
            if (actualMountainCount == 0)
                return;

            IList<(int, int)> emptyCoords = new List<(int, int)>();
            for (int x = 0; x < MGP.Size; x++)
                for (int y = 0; y < MGP.Size; y++)
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
            {
                tiles[mountainCoord.Item1, mountainCoord.Item2] = new RockTile();
            }
        }

        private void GenerateChunk((int, int) offset)
        {
            if (new System.Random().NextDouble() < MGP.DesertChunkProbability)
            {
                GenerateDesert(offset);
                return;
            }

            GenerateGrassLand(offset);
        }

        private void GenerateDesert((int, int) offset)
        {
            IList<TileBase> startingTileContenders = new List<TileBase>();
            for (int x = offset.Item1 * MGP.Size / 2; x < (offset.Item1 + 1) * MGP.Size / 2; x++)
            {
                for (int y = offset.Item2 * MGP.Size / 2; y < (offset.Item2 + 1) * MGP.Size / 2; y++)
                {
                    if (tiles[x, y] == null)
                    {
                        TileBase sand = new SandTile();
                        tiles[x, y] = sand;
                        startingTileContenders.Add(sand);
                    }
                }
            }

            System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
            GameManager.Get<MapManagerBase>().StartingTiles.Add(startingTileContenders[rand.Next(startingTileContenders.Count)]);
        }

        private void GenerateGrassLand((int, int) offset)
        {
            IList<(int, int, float)> emptyCoords = new List<(int, int, float)>();
            for (int x = offset.Item1 * MGP.Size / 2; x < (offset.Item1 + 1) * MGP.Size / 2; x++)
                for (int y = offset.Item2 * MGP.Size / 2; y < (offset.Item2 + 1) * MGP.Size / 2; y++)
                    if (tiles[x, y] == null)
                        emptyCoords.Add((x, y, noiseMap[x, y]));

            int actualForestCount = new System.Random(System.DateTime.Now.Millisecond).Next(
                MGP.MaxForestCountPerChunk - MGP.MinForestCountPerChunk + 1)
                + MGP.MinForestCountPerChunk;

            foreach ((int, int) forestCoord in
                emptyCoords.OrderBy(x => x.Item3)
                    .TakeLast(System.Math.Min(emptyCoords.Count, actualForestCount))
                    .Select(x => (x.Item1, x.Item2)))
            {
                tiles[forestCoord.Item1, forestCoord.Item2] = new ForestTile();
            }


            IList<TileBase> startingTileContenders = new List<TileBase>();
            foreach ((int, int) coord in emptyCoords.Select(x => (x.Item1, x.Item2)))
            {
                if (tiles[coord.Item1, coord.Item2] == null)
                {
                    TileBase grass = new GrassTile();
                    tiles[coord.Item1, coord.Item2] = grass;
                    startingTileContenders.Add(grass);
                }
            }

            System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
            GameManager.Get<MapManagerBase>().StartingTiles.Add(startingTileContenders[rand.Next(startingTileContenders.Count)]);
        }

        private void SetupCoordinateSystem()
        {
            for (int x = 0; x < MGP.Size; x++)
                for (int y = 0; y < MGP.Size; y++)
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
                coordinate.Item1 < MGP.Size &&
                coordinate.Item2 < MGP.Size;
        }
    }
}