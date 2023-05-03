using Controller;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(TMP_Dropdown))]
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
            Model.GameManager.Get<Model.TurnManagerBase>().OnTurnStarted += UpdateContent;
            Model.GameManager.Get<Model.BuildManagerBase>().OnBuildingBuilt += UpdateContent;
            Model.GameManager.Get<Model.TrainManagerBase>().OnTroopTrained += UpdateContent;
            Model.GameManager.Get<Model.TechTreeManagerBase>().OnTechUnlocked += UpdateContent;
            View.BuildPanelController.Instance.OnBuildPanelRevealed += UpdateContent;

            View.BuildingManager.Instance.OnBuildAttempted += SetSelected;
        }

        private void UpdateContent(Model.Player player)
        {
            if (View.TroopManager.Instance.SelectedTroop == null)
                return;

            dropdown.ClearOptions();
            IList<string> availableBuildings = player.AvailableBuildings;

            foreach (string buildingName in availableBuildings)
            {
                if (player.ResourceContainer.HasEnoughFor(Model.Cost.CreateNewFromJsonCost(Model.BuildingBase.BuildingProperties[buildingName].Cost))
                    && View.TroopManager.Instance.SelectedTroop.Buildings.Contains(buildingName))
                    dropdown.options.Add(new TMP_Dropdown.OptionData() { text = buildingName });
            }
            dropdown.value = 0;
            dropdown.RefreshShownValue();
        }

        private void SetSelected()
        {
            View.BuildingManager.Instance.Blueprint = dropdown.options.Count > 0 ?
                buildings[dropdown.options[dropdown.value].text] : null;
        }
    }
}