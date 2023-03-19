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
        public event Action<Tile> OnTileSelected;

        [SerializeField] private int size;
        public int Size { get => size; }

        [SerializeField] private float waterProbability;
        public float WaterProbability { get => waterProbability; }

        private Model.TileBase[,] tiles;

        public Dictionary<Tile, Model.TileBase> ViewToModelMap { get; }
            = new Dictionary<Tile, Model.TileBase>();

        public Dictionary<Model.TileBase, Tile> ModelToViewMap { get; }
            = new Dictionary<Model.TileBase, Tile>();

        private Tile selectedTile;
        public Tile SelectedTile
        {
            get => selectedTile;
            set
            {
                selectedTile = value;
                OnTileSelected?.Invoke(selectedTile);
                HighlightManager.Instance.FireMonoBehaviourSelectedEvent(selectedTile);
            }
        }

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

            HighlightManager.Instance.MonoBehaviourSelected += (monoBehaviour) =>
            {
                if (selectedTile == monoBehaviour)
                    return;

                Tile original = selectedTile;
                selectedTile = null;
                original?.Deselect();
            };
        }

        private void MapViewToModel(Tile[,] viewTiles)
        {
            int size = tiles.GetLength(0);
            for (int x = 0; x < size; ++x)
            {
                for (int y = 0; y < size; ++y)
                {
                    ViewToModelMap[viewTiles[x, y]] = tiles[x, y];
                    ModelToViewMap[tiles[x, y]] = viewTiles[x, y];
                }
            }

            OnViewMappedToModel?.Invoke();
        }
    }
}