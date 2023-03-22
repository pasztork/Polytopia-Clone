using System;

namespace Model
{
    public class MapManager
    {
        public static MapManager instance;
        public static MapManager Instance
        {
            get
            {
                instance ??= new MapManager();
                return instance;
            }
        }

        private int size;
        public int Size
        {
            set
            {
                size = value;
                tiles = new TileBase[size, size];
            }
        }

        private TileBase[,] tiles;
        public TileBase[,] Tiles
        {
            get => tiles;
            private set => tiles ??= value;
        }

        public TileBase SelectedTile { get; set; }

        public void GenerateMap()
        {
            MapGenerator.Instance.GenerateMap();
        }

        public void LoadMap()
        {
            throw new NotImplementedException();
        }
    }
}