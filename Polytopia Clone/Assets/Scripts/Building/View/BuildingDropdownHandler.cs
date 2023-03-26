using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    [RequireComponent(typeof(Dropdown))]
    public class BuildingDropdownHandler : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<string, BuildingBase> buildings;
        private TMP_Dropdown dropdown = null;

        private void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }

        private void Start()
        {
            Model.TurnManager.Instance.OnTurnStarted += UpdateContent;
            Model.BuildManager.Instance.OnBuildingBuilt += UpdateContent;
            Model.TrainManager.Instance.OnTroopTrained += UpdateContent;
            Controller.BuildingManager.Instance.OnBuildAttempted += SetSelected;
        }

        private void UpdateContent(Model.Player player)
        {
            dropdown.ClearOptions();
            IList<string> availableBuildings = player.AvailableBuildings;
            foreach (string buildingName in availableBuildings)
            {
                BuildingBase building = buildings[buildingName];
                Model.Cost cost = new Model.Cost(building.Cost.MoneyCost, building.Cost.MaterialCost, building.Cost.FoodCost);
                if (player.ResourceContainer.HasEnoughFor(cost))
                    dropdown.options.Add(new TMP_Dropdown.OptionData() { text = buildingName });
            }
            dropdown.value = 0;
            dropdown.RefreshShownValue();
        }

        private void SetSelected()
        {
            Controller.BuildingManager.Instance.Blueprint = dropdown.options.Count > 0 ?
                buildings[dropdown.options[dropdown.value].text] : null;
        }
    }
}