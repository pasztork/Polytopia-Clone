using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    private Tile[,] tiles;
    public Tile[,] Tiles
    {
        get => tiles;
        set => tiles ??= value;
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