using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Grass Chunk")]
    [SerializeField] private GameObject grassTile;

    [Header("Sand Chunk")]
    [SerializeField] private GameObject sandTile;

    [Header("Water Tile Prefab")]
    [SerializeField] private GameObject waterTile;

    [Header("Mountain Tile Prefab")]
    [SerializeField] private GameObject mountainTile;

    [Header("Map Generation Settings")]
    [SerializeField] private int size;
    [SerializeField] private float scale;
    [SerializeField] private float waterProbability;
    [SerializeField] private float mountainProbability;

    private Tile[,] tiles;

    private void Start()
    {
        if (size % 2 == 1)
        {
            throw new System.ArgumentException("size must be even");
        }

        MapManager.Instance.Tiles = new Tile[size, size];
        tiles = MapManager.Instance.Tiles;
        GenerateMap();
    }

    // TODO: Don't include in release
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DestroyMap();
            GenerateMap();
        }
    }

    private void DestroyMap()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Destroy(tiles[x, y]);
            }
        }
    }

    private void GenerateMap()
    {
        Vector3 tileSize = grassTile.transform.localScale;

        float[,] noiseMap = GenerateNoiseMap();
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                tiles[x, y] = noiseMap[x, y] < waterProbability
                    ? Instantiate(waterTile, new Vector3(x * tileSize.x, 0f, y * tileSize.z), Quaternion.identity).GetComponent<Tile>()
                    : null;
            }
        }

        (int, int)[] offsets = { (0, 0), (0, 1), (1, 0), (1, 1) };
        foreach ((int, int) offset in offsets)
        {
            GameObject chunkTile = PickChunk();
            for (int x = offset.Item1 * size / 2; x < (offset.Item1 + 1) * size / 2; x++)
            {
                for (int y = offset.Item2 * size / 2; y < (offset.Item2 + 1) * size / 2; y++)
                {
                    if (tiles[x, y] == null)
                    {
                        GameObject tile = noiseMap[x, y] > 1 - mountainProbability ? mountainTile : chunkTile;
                        tiles[x, y] = Instantiate(tile, new Vector3(x * tileSize.x, 0f, y * tileSize.z), Quaternion.identity).GetComponent<Tile>();
                    }
                }
            }
        }

        SetupCoordinateSystem();
        MapManager.Instance.Tiles = tiles;
    }

    private GameObject PickChunk()
    {
        int random = Random.Range(0, 2);
        return random == 0 ? grassTile : sandTile;
    }

    private float[,] GenerateNoiseMap()
    {
        float[,] noiseMap = new float[size, size];
        float xOffset = Random.Range(-10000f, 10000f);
        float zOffset = Random.Range(-10000f, 10000f);
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x * scale + xOffset, y * scale + zOffset);
                noiseMap[x, y] = noiseValue;
            }
        }
        return noiseMap;
    }

    private void SetupCoordinateSystem()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                AddNeighborsToList(x, y);
            }
        }
    }

    private void AddNeighborsToList(int x, int y)
    {
        Tile tile = tiles[x, y].GetComponent<Tile>();
        (int, int)[] neighborCoordinates = {
                        (x - 1, y),
            (x, y - 1),             (x, y + 1),
                        (x + 1, y)
        };

        foreach ((int, int) coordinate in neighborCoordinates)
        {
            if (IsValidCoordinate(coordinate))
            {
                tile.Neighbors.Add(tiles[coordinate.Item1, coordinate.Item2]);
            }
        }
    }

    private bool IsValidCoordinate((int, int) coordinate)
    {
        return
            coordinate.Item1 >= 0 &&
            coordinate.Item2 >= 0 &&
            coordinate.Item1 < size &&
            coordinate.Item2 < size;
    }
}
