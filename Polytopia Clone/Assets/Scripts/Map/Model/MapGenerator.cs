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
        private float waterTileProbability;

        public void GenerateMap(float waterTileProbability)
        {

            tiles = MapManager.Instance.Tiles;
            size = MapManager.Instance.Tiles.GetLength(0);
            this.waterTileProbability = waterTileProbability;

            noiseMap = PerlinNoise.GenerateNoiseMap(size);
            GenerateWaterTiles();
            FillEmptyTiles();
            SetupCoordinateSystem();
        }

        private void GenerateWaterTiles()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; ++y)
                    tiles[x, y] = noiseMap[x, y] < waterTileProbability
                        ? new WaterTile()
                        : null;
        }

        private void FillEmptyTiles()
        {
            (int, int)[] offsets = { (0, 0), (0, 1), (1, 0), (1, 1) };
            foreach ((int, int) offset in offsets)
            {
                string chunkType = PickChunk();
                for (int x = offset.Item1 * size / 2; x < (offset.Item1 + 1) * size / 2; ++x)
                    for (int y = offset.Item2 * size / 2; y < (offset.Item2 + 1) * size / 2; ++y)
                        tiles[x, y] ??= GetNewTile(chunkType);
            }
        }

        private string PickChunk() =>
            new System.Random().Next(2) == 0
                ? "Grass"
                : "Sand";

        private TraversableTile GetNewTile(string chunkType) =>
            chunkType == "Grass"
                ? new GrassTile()
                : new SandTile();

        private void SetupCoordinateSystem()
        {
            for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y)
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