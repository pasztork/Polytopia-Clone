using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class BuildingDropdownHandler : MonoBehaviour
{
    public static BuildingDropdownHandler Instance { get; private set; }

    private Dictionary<string, BuildingBase> buildings = new Dictionary<string, BuildingBase>();

    public BuildingBase SelectedItem
    {
        get
        {
            int index = dropdown.value;
            return dropdown.options.Count > 0 ? buildings[dropdown.options[index].text] : null;
        }
    }

    private TMP_Dropdown dropdown = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one BuildingDropdownHandler in scene!");
            return;
        }
        Instance = this;
        dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        TurnManager.Instance.StartTurn += UpdateContent;
    }

    public void UpdateContent()
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
}