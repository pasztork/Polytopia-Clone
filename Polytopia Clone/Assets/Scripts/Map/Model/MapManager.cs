using System;
using System.Collections.Generic;

namespace Model
{
    public class MapManager
    {
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

        public IList<TileBase> StartingTiles { get; } = new List<TileBase>();

        public TileBase SelectedTile { get; set; }

        public void GenerateMap()
        {
            DependencyContainer.Get<MapGenerator>().GenerateMap();
        }

        public void LoadMap()
        {
            throw new NotImplementedException();
        }

        public TileBase GetStartingTile()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            TileBase tile = StartingTiles[rand.Next(StartingTiles.Count)];
            StartingTiles.Remove(tile);
            return tile;
        }
    }
}