using System;
using System.Linq;
using UnityEngine;

namespace ReplayView
{
    public class MapBuilder : MonoBehaviour
    {
        private static MapBuilder instance;
        public static MapBuilder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<MapBuilder>();
                }
                return instance;
            }
        }

        public event Action<Tile[,]> OnMapBuilt;

        [SerializeField] private SerializableDictionary<string, Tile> tileBlueprints;
        private Vector3 tileSize;
        private Tile[,] viewTiles;
        private Model.TileBase[,] modelTiles;
        private int size;

        public void BuildMapGFX(Model.TileBase[,] modelTiles)
        {
            this.modelTiles = modelTiles;
            size = this.modelTiles.GetLength(0);
            viewTiles = new Tile[size, size];

            tileSize = GetTileSize();
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    SetupTile(x, y);

            OnMapBuilt?.Invoke(viewTiles);
            SetupNeighbors();
        }

        private Tile GetTileGFX(Model.TileBase tile) =>
            tileBlueprints[tile.ToString()];

        private Vector3 GetTileSize() =>
            tileBlueprints.Values.First().transform.localScale;

        private void SetupTile(int x, int y)
        {
            viewTiles[x, y] = Instantiate(
                GetTileGFX(modelTiles[x, y]),
                new Vector3(x * tileSize.x, 0f, y * tileSize.z),
                Quaternion.identity);
        }

        private void SetupNeighbors()
        {
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    AddNeighborsToList(x, y);
        }

        private void AddNeighborsToList(int x, int y)
        {
            Tile tile = viewTiles[x, y];
            (int, int)[] neighborCoordinates = {
            (x - 1, y - 1), (x - 1, y), (x - 1, y + 1),
            (x, y - 1),                 (x, y + 1),
            (x + 1, y - 1), (x + 1, y), (x + 1, y + 1)
        };
            foreach ((int, int) coordinate in neighborCoordinates)
                if (IsValidCoordinate(coordinate))
                    tile.Neighbors.Add(viewTiles[coordinate.Item1, coordinate.Item2]);
        }

        private bool IsValidCoordinate((int, int) coordinate)
        {
            return
                coordinate.Item1 >= 0 &&
                coordinate.Item2 >= 0 &&
                coordinate.Item1 < size &&
                coordinate.Item2 < size;
        }

        public Tile GetTileByCoord(int x, int y)
        {
            return viewTiles[x, y];
        }
    }
}