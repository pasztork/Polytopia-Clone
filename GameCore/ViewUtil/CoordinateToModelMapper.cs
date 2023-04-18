using System.Collections.Generic;
using System.Data;

namespace ViewUtil
{
    public class CoordinateToModelMapper
    {
        private readonly IDictionary<(int, int), Model.TileBase> _tileMap =
            new Dictionary<(int, int), Model.TileBase>();

        public CoordinateToModelMapper()
        {
            VerifyMapExists();
            MapCoordinatesToTiles();
        }

        public Model.TileBase GetTileAt(int x, int y) => _tileMap[(x, y)];

        public Model.BuildingBase GetBuildingAt(int x, int y) => GetTileAt(x, y).BuildingOnTop;

        public Model.TroopBase GetTroopAt(int x, int y) => GetTileAt(x, y).TroopOnTop;

        private void VerifyMapExists()
        {
            if (Model.GameManager.Get<Model.MapManagerBase>().Tiles is null)
            {
                throw new ConstraintException("No map found!");
            }
        }

        private void MapCoordinatesToTiles()
        {
            Model.TileBase[,] tiles = Model.GameManager.Get<Model.MapManagerBase>().Tiles;

            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                for (int y = 0; y < tiles.GetLength(1); y++)
                {
                    _tileMap.Add((x, y), tiles[x, y]);
                }
            }
        }
    }
}