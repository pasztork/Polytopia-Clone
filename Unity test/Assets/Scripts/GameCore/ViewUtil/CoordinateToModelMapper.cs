using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ViewUtil
{
    public class CoordinateToModelMapper
    {
        private readonly IDictionary<(int, int), Model.TileBase> _coordinatesToTileMap =
            new Dictionary<(int, int), Model.TileBase>();

        private readonly IDictionary<Model.TileBase, (int, int)> _tileToCoordinatesMap =
            new Dictionary<Model.TileBase, (int, int)>();

        public CoordinateToModelMapper()
        {
            VerifyMapExists();
            MapCoordinatesToTiles();
        }

        public Model.TileBase GetTileAt(int x, int y) => _coordinatesToTileMap[(x, y)];

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
                    _coordinatesToTileMap.Add((x, y), tiles[x, y]);
                    _tileToCoordinatesMap.Add(tiles[x, y], (x, y));
                }
            }
        }

        public (int, int) GetCoordinatesOf(Model.TileBase tile) => _tileToCoordinatesMap[tile];
        public int[] GetCoordinatesOf(IList<Model.TileBase> tiles)
        {
            int[] coordinates = new int[2 * tiles.Count];
            foreach(var tile in tiles)
            {
                (int, int) tileCoords = GetCoordinatesOf(tile);
                coordinates.Append(tileCoords.Item1);
                coordinates.Append(tileCoords.Item2);
            }
            return coordinates;
        }
    }
}