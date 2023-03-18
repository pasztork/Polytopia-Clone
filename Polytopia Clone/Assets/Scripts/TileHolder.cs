using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileHolder : MonoBehaviour
{
    private BuildingBase buildingOnTop;
    public BuildingBase BuildingOnTop { 
        get => buildingOnTop; 
        set 
        {
            if (buildingOnTop != null)
            {
                buildingOnTop.OnBuildingClick -= OnTrainAvailable;
            }
            buildingOnTop = value;
            if (buildingOnTop != null)
            {
                buildingOnTop.OnBuildingClick += OnTrainAvailable;
            }
        } 
    }

    public TroopBase TroopOnTop { get; set; }

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
