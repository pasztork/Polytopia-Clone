using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than one UIManager in scene!");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        TurnManager.Instance.StartTurn += HideAll;
        Plane.Instance.OnPlaneClick += HideAll;
        BuildManager.Instance.OnBuild += HideAll;
    }

    public void HideAll()
    {
        MapManager.Instance.SelectedTile = null;
        BuildManager.Instance.ActiveBuildingHolder = null;
    }
}
