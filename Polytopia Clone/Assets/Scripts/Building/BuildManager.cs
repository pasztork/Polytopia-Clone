using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    // This could be a Singleton of it's own.
    // It still makes more sence to be a part of the BuildManager.
    [SerializeField] private BuildingBlueprintHolder buildingBlueprints;
    public BuildingBlueprintHolder BuildingBlueprints { get => buildingBlueprints; }

    public ResourceContainer ActiveResourceContainer { get; set; } = null;
    public BuildingHolder ActiveBuildingHolder { get; set; } = null;

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

        if (CanBuild(buildingBlueprint))
        {
            BuildingBase buildingInstance =
                Instantiate(buildingBlueprint,
                    ActiveBuildingHolder.transform.position + new Vector3(0f, 1f, 0f),
                    Quaternion.identity);
            ActiveBuildingHolder.BuildingOnTop = buildingInstance;
            ActiveResourceContainer -= buildingBlueprint.Cost;
            ActiveBuildingHolder = null;
            TurnManager.Instance.CurrentPossibleActions["Build"]--;
            VisibilityManager.HideAll();
            BuildingDropdownHandler.Instance.UpdateContent();
        }
    }

    private bool CanBuild(BuildingBase blueprint)
    {
        Cost cost = blueprint.Cost;
        return
            ActiveBuildingHolder != null &&
            ActiveBuildingHolder.BuildingOnTop == null &&
            ActiveResourceContainer.HasEnoughFor(cost) &&
            TurnManager.Instance.CurrentPossibleActions["Build"] > 0;
    }
}
