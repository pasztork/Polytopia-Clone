using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }

        public event Action OnViewMappedToModel;

        [SerializeField] private int size;
        public int Size { get => size; }

        [SerializeField] private int minMountainCount;
        public int MinMountainCount { get => minMountainCount; }

        [SerializeField] private int maxMountainCount;
        public int MaxMountainCount { get => maxMountainCount; }

        [SerializeField] private int minForrestCountPerChunk;
        public int MinForrestCountPerChunk { get => minForrestCountPerChunk; }

        [SerializeField] private int maxForrestCountPerChunk;
        public int MaxForrestCountPerChunk { get => maxForrestCountPerChunk; }

        [SerializeField] private float desertChunkProbability;
        public float DesertChunkProbability { get => desertChunkProbability; }

        [SerializeField] private float waterTileProbability;
        public float WaterTileProbability { get => waterTileProbability; }

        private Model.TileBase[,] tiles;

        public Dictionary<View.Tile, Model.TileBase> ViewToModelMap { get; }
            = new Dictionary<View.Tile, Model.TileBase>();

        public Dictionary<Model.TileBase, View.Tile> ModelToViewMap { get; }
            = new Dictionary<Model.TileBase, View.Tile>();

        private View.Tile selectedTile;
        public View.Tile SelectedTile
        {
            get => selectedTile;
            set
            {
                selectedTile = value;
                if (selectedTile != null)
                    View.HighlightManager.Instance.FireMonoBehaviourSelectedEvent(selectedTile);
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
            View.MapBuilder.Instance.OnMapBuilt += MapViewToModel;
            View.MapBuilder.Instance.BuildMapGFX(tiles);

            View.HighlightManager.Instance.OnMonoBehaviourSelected += (monoBehaviour) =>
            {
                if (selectedTile == monoBehaviour)
                    return;

                View.Tile original = selectedTile;
                selectedTile = null;
                original?.Deselect();
            };
        }

        private void MapViewToModel(View.Tile[,] viewTiles)
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