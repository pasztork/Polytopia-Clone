using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    // This might not be needed
    public Tile SelectedTile { get; set; }

    public ResourceContainer ActiveResourceContainer { get; set; } = null;
    public BuildingHolder ActiveBuildingHolder { get; set; } = null;

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
        Cost cost = buildingBlueprint.GetComponent<Bank>().Cost;
        if (ActiveBuildingHolder != null && ActiveResourceContainer.HasEnoughFor(cost))
        {
            ActiveResourceContainer -= cost;
            ActiveBuildingHolder = null;
            Instantiate(buildingBlueprint, ActiveBuildingHolder.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            VisibilityManager.HideAll();
        }
    }

    public void DeployTroop()
    {
        Instantiate(troopBlueprint, ActiveBuildingHolder.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
        VisibilityManager.HideAll();
    }
}
