using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Tile types on map")]
    [SerializeField] private GameObject[] tileTypes;

    [Header("Map size")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    private GameObject[,] tiles;

    private void Start()
    {
        tiles = new GameObject[width, height];
        Vector3 tileSize = tileTypes[0].transform.localScale;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                tiles[x, z] = Instantiate(PickTileOnRandom(), new Vector3(x * tileSize.x, 0f, z * tileSize.z), Quaternion.identity);
            }
        }
        SetupCoordinateSystem();
    }

    private GameObject PickTileOnRandom()
    {
        return tileTypes[Random.Range(0, tileTypes.Length)];
    }

    private void SetupCoordinateSystem()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                AddNeighborsToList(x, z);
            }
        }
    }

    private void AddNeighborsToList(int x, int z)
    {
        Tile tile = tiles[x, z].GetComponent<Tile>();
        (int, int)[] neighborCoordinates = {
            (x - 1, z - 1), (x - 1, z), (x - 1, z + 1),
            (x, z - 1),                 (x, z + 1),
            (x + 1, z - 1), (x + 1, z), (x + 1, z + 1),
        };

        foreach ((int, int) coordinate in neighborCoordinates)
        {
            if (isValidCoordinate(coordinate))
            {
                tile.Neighbors.Add(tiles[coordinate.Item1, coordinate.Item2]);
            }
        }
    }

    private bool isValidCoordinate((int, int) coordinate)
    {
        return
            coordinate.Item1 >= 0 &&
            coordinate.Item2 >= 0 &&
            coordinate.Item1 < width &&
            coordinate.Item2 < height;
    }
}
