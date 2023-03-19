using UnityEngine;
using UnityEngine.EventSystems;

public class TileHolder : MonoBehaviour
{
    private BuildingBase buildingOnTop;
    public BuildingBase BuildingOnTop
    {
        get => buildingOnTop;
        set
        {
            if (buildingOnTop != null)
            {
                buildingOnTop.OnBuildingClicked -= OnTrainAvailable;
            }
            buildingOnTop = value;
            if (buildingOnTop != null)
            {
                buildingOnTop.OnBuildingClicked += OnTrainAvailable;
            }
        }
    }

    public TroopBase TroopOnTop;

    public bool HasBuilding { get => BuildingOnTop == null; }

    public bool HasTroop { get => TroopOnTop == null; }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        BuildManager.Instance.ActiveTileHolder = this;
    }

    private void OnTrainAvailable()
    {
        TrainManager.Instance.ActiveTileHolder = this;
    }
}
