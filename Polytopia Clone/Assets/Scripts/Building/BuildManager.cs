using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    // This might not be needed
    public Tile SelectedTile { get; set; }

    public ResourceContainer ActiveResourceContainer { get; set; } = null;
    public BuildingHolder ActiveBuildingHolder { get; set; } = null;

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
        BuildingBase buildingBlueprint = BuildingDropdownHandler.Instance.SelectedItem;
        if (buildingBlueprint == null)
        {
            return;
        }
        Cost cost = buildingBlueprint.Cost;
        if (ActiveBuildingHolder != null && ActiveBuildingHolder.BuildingOnTop == null && ActiveResourceContainer.HasEnoughFor(cost))
        {
            BuildingBase buildingInstance = Instantiate(buildingBlueprint, ActiveBuildingHolder.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            ActiveBuildingHolder.BuildingOnTop = buildingInstance;
            ActiveResourceContainer -= cost;
            ActiveBuildingHolder = null;
            VisibilityManager.HideAll();
            BuildingDropdownHandler.Instance.UpdateContent();
        }
    }

    public void DeployTroop()
    {
        Instantiate(troopBlueprint, ActiveBuildingHolder.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
        VisibilityManager.HideAll();
    }
}
