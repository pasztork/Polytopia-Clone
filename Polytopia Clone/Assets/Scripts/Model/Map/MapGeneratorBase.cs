using System;

namespace Model
{
    public abstract class MapGeneratorBase
    {
        public MapGenerationProperties MGP { protected get; set; }

        public event Action<TileBase, int, int> TileGenerated;

        public void TriggerTileCreated(TileBase tile, int x, int y)
        {
            TileGenerated?.Invoke(tile, x, y);
        }

        public abstract void GenerateMap();
    }
}