using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Controller
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }

        public event Action OnViewMappedToModel;

        private Model.TileBase[,] tiles;

        private Dictionary<Tile, Model.TileBase> viewToModelMap;

        public Tile SelectedTile { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one MapManager in scene!");
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            tiles = Model.MapManager.Instance.Tiles;
            MapBuilder.Instance.OnMapBuilt += MapViewToModel;
            MapBuilder.Instance.BuildMapGFX(tiles);
        }

        private void MapViewToModel(Tile[,] viewTiles)
        {
            viewToModelMap = new Dictionary<Tile, Model.TileBase>();
            int size = tiles.GetLength(0);
            for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y)
                    viewToModelMap[viewTiles[x, y]] = tiles[x, y];

            OnViewMappedToModel?.Invoke();
        }
    }
}