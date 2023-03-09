using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    private Tile selectedTile;
    public Tile SelectedTile
    {
        get { return selectedTile; }
        set { selectedTile = value; }
    }

    [SerializeField] private GameObject buildingBlueprint;
    [SerializeField] private GameObject troopBlueprint;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        Instance = this;
    }

    public void Build()
    {
        Debug.Log("Build");
        Instantiate(buildingBlueprint, selectedTile.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        BuildCanvas.Instance.Hide();
    }

    public void DeployTroop()
    {
        Debug.Log("Deploy Troop");
        Instantiate(troopBlueprint, selectedTile.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
        BuildCanvas.Instance.Hide();
    }
}
