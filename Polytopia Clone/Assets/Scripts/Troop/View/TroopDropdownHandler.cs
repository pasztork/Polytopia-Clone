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
            Model.TurnManager.Instance.OnTurnStarted += UpdateContent;
            Model.TrainManager.Instance.OnTroopTrained += UpdateContent;
            Model.BuildManager.Instance.OnBuildingBuilt += UpdateContent;
            Controller.TroopManager.Instance.OnTrainAttempted += SetSelected;
        }

        private void UpdateContent(Model.Player player)
        {
            dropdown.ClearOptions();
            IList<string> availableTroops = player.AvailableTroops;
            foreach (string troopName in availableTroops)
            {
                TroopBase troop = troops[troopName];
                Model.Cost cost = new Model.Cost(troop.Cost.MoneyCost, troop.Cost.MaterialCost, troop.Cost.FoodCost);
                if (player.ResourceContainer.HasEnoughFor(cost) && Model.TurnManager.Instance.CurrentActionCount["Train"] > 0)
                    dropdown.options.Add(new TMP_Dropdown.OptionData() { text = troopName });
            }
            dropdown.value = 0;
            dropdown.RefreshShownValue();
        }

        private void SetSelected()
        {
            Controller.TroopManager.Instance.Blueprint = dropdown.options.Count > 0 ?
                troops[dropdown.options[dropdown.value].text] : null;
        }
    }
}