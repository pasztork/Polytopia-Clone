using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class TroopDropdownHandler : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<string, TroopBase> troops;
        private TMP_Dropdown dropdown = null;

        private void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }

        private void Start()
        {
            Model.GameManager.Get<Model.TurnManagerBase>().OnTurnStarted += UpdateContent;
            Model.GameManager.Get<Model.TrainManagerBase>().OnTroopTrained += UpdateContent;
            Model.GameManager.Get<Model.BuildManagerBase>().OnBuildingBuilt += UpdateContent;
            Model.GameManager.Get<Model.TechTreeManagerBase>().OnTechUnlocked += UpdateContent;
            View.TrainPanelController.Instance.OnTrainPanelRevealed += UpdateContent;

            View.TroopManager.Instance.OnTrainAttempted += SetSelected;
        }

        private void UpdateContent(Model.Player player)
        {
            if (View.BuildingManager.Instance.SelectedBuilding == null)
                return;

            dropdown.ClearOptions();
            IList<string> availableTroops = player.AvailableTroops;
            foreach (string troopName in availableTroops)
            {
                if (player.ResourceContainer.HasEnoughFor(Model.Cost.CreateNewFromJsonCost(Model.TroopBase.TroopProperties[troopName].Cost))
                    && View.BuildingManager.Instance.SelectedBuilding.Troops.Contains(troopName))
                    dropdown.options.Add(new TMP_Dropdown.OptionData() { text = troopName });
            }
            dropdown.value = 0;
            dropdown.RefreshShownValue();
        }

        private void SetSelected()
        {
            View.TroopManager.Instance.Blueprint = dropdown.options.Count > 0 ?
                troops[dropdown.options[dropdown.value].text] : null;
        }
    }
}