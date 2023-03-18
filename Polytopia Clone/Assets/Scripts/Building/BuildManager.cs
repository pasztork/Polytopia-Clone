using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    public event Action OnBuild;
    public event Action OnBuildAttempted;

    // This could be a Singleton of it's own.
    // It still makes more sence to be a part of the BuildManager.
    [SerializeField] private BuildingBlueprintHolder buildingBlueprints;
    public BuildingBlueprintHolder BuildingBlueprints { get => buildingBlueprints; }

    public ResourceContainer ActiveResourceContainer { get; set; }
    public TileHolder ActiveTileHolder { get; set; }

    public BuildingBase Blueprint { private get; set; }

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
        // Should throw error if there are no subscribers.
        OnBuildAttempted.Invoke();
        if (Blueprint == null)
            return;

        if (CanBuild(Blueprint))
        {
            BuildingBase buildingInstance =
                Instantiate(Blueprint,
                    ActiveTileHolder.transform.position + new Vector3(0f, 1f, 0f),
                    Quaternion.identity);
            ActiveTileHolder.BuildingOnTop = buildingInstance;
            ActiveResourceContainer -= Blueprint.Cost;
            ActiveTileHolder = null;

            OnBuild?.Invoke();
        }
    }

    private bool CanBuild(BuildingBase blueprint)
    {
        return
            (ActiveTileHolder != null ? ActiveTileHolder.HasBuilding : false) &&
            ActiveResourceContainer.HasEnoughFor(blueprint.Cost) &&
            TurnManager.Instance.CurrentPossibleActions["Build"] > 0;
    }
}
