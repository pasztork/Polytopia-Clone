using System;

namespace Model
{
    public class MapManager : MapManagerBase
    {
        public override void GenerateMap()
        {
            GameManager.Get<MapGeneratorBase>().GenerateMap();
        }

        public override void LoadMap()
        {
            throw new NotImplementedException();
        }

        public override TileBase GetStartingTile()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            TileBase tile = StartingTiles[rand.Next(StartingTiles.Count)];
            StartingTiles.Remove(tile);
            return tile;
        }
    }
}