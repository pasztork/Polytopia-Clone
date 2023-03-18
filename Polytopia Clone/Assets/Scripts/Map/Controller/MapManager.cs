using UnityEngine;

namespace Controller
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }

        private Model.TileBase[,] tiles;
        public Model.TileBase[,] Tiles
        {
            get
            {
                tiles ??= Model.MapManager.Instance.Tiles;
                return tiles;
            }

            private set => tiles = value;
        }

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
    }
}