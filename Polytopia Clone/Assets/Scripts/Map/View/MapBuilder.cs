using System.Linq;
using UnityEngine;

namespace View
{
    public class MapBuilder : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<string, Tile> tileBlueprints;
        private Tile[,] viewTiles;
        private Model.TileBase[,] modelTiles;
        private int size;

        private void Start()
        {
            modelTiles = Controller.MapManager.Instance.Tiles;
            size = modelTiles.GetLength(0);
            viewTiles = new Tile[size, size];
            BuildMapGFX();
        }

        private void BuildMapGFX()
        {
            Vector3 tileSize = GetTileSize();
            for (int x = 0; x < size; ++x)
                for (int y = 0; y < size; ++y)
                    viewTiles[x, y] = Instantiate(
                        GetTileGFX(modelTiles[x, y]),
                        new Vector3(x * tileSize.x, 0f, y * tileSize.z),
                        Quaternion.identity);
        }

        private Tile GetTileGFX(Model.TileBase tile) =>
            tileBlueprints[tile.ToString()];

        private Vector3 GetTileSize() =>
            tileBlueprints.Values.First().transform.localScale;
    }
}