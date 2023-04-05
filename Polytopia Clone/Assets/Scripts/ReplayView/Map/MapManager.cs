using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReplayView
{
    public class MapManager : MonoBehaviour
    {
        private static MapManager instance;
        public static MapManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<MapManager>();
                }
                return instance;
            }
        }

        public event Action OnViewMappedToModel;

        private Model.TileBase[,] tiles;

        public Dictionary<Tile, Model.TileBase> ViewToModelMap { get; }
            = new Dictionary<Tile, Model.TileBase>();

        public Dictionary<Model.TileBase, Tile> ModelToViewMap { get; }
            = new Dictionary<Model.TileBase, Tile>();

        private void Start()
        {
            tiles = Model.GameManager.Get<Model.MapManagerBase>().Tiles;
            MapBuilder.Instance.OnMapBuilt += MapViewToModel;
            MapBuilder.Instance.BuildMapGFX(tiles);
        }

        private void MapViewToModel(Tile[,] viewTiles)
        {
            int size = tiles.GetLength(0);
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    ViewToModelMap[viewTiles[x, y]] = tiles[x, y];
                    ModelToViewMap[tiles[x, y]] = viewTiles[x, y];
                }
            }

            OnViewMappedToModel?.Invoke();
        }
    }
}
