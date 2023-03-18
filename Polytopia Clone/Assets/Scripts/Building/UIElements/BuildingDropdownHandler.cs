using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class BuildingDropdownHandler : MonoBehaviour
{
    private Dictionary<string, BuildingBase> buildings;
    private TMP_Dropdown dropdown = null;

    private void Awake()
    {
        buildings = new Dictionary<string, BuildingBase>();
        dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        TurnManager.Instance.StartTurn += UpdateContent;
        BuildManager.Instance.OnBuild += UpdateContent;
        BuildManager.Instance.OnBuildAttempted += SetSelected;
    }

    private void UpdateContent()
    {
        dropdown.ClearOptions();
        buildings.Clear();
        IList<BuildingBase> availableBuildingBlueprints = TurnManager.Instance.CurrentPlayer.AvailableBuildingBlueprints;
        foreach (BuildingBase building in availableBuildingBlueprints)
        {
            if (TurnManager.Instance.CurrentPlayer.GetComponent<ResourceContainer>().HasEnoughFor(building.Cost) &&
                TurnManager.Instance.CurrentPossibleActions["Build"] > 0)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = building.name });
                buildings.Add(building.name, building);
            }
        }
        dropdown.value = 0;
        dropdown.RefreshShownValue();
    }

    private void SetSelected()
    {
        int index = dropdown.value;
        BuildManager.Instance.Blueprint = dropdown.options.Count > 0 ?
            buildings[dropdown.options[index].text] : null;
    }
}