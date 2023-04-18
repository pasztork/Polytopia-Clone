using System.Collections.Generic;

namespace Model
{
    public abstract class MapManagerBase
    {
        public string MapFilePath { get; protected set; } = null;

        private TileBase[,] tiles;
        public TileBase[,] Tiles
        {
            get => tiles;
            set => tiles ??= value;
        }

        public IList<TileBase> StartingTiles { get; } = new List<TileBase>();

        public TileBase SelectedTile { get; set; }

        public abstract void GenerateMap();

        public abstract void LoadMap(string filePath);

        public abstract TileBase GetStartingTile();
    }
}