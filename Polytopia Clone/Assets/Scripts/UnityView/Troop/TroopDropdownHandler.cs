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

        private Dictionary<string, Model.JsonCost> troopCosts = null;

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

            View.TroopManager.Instance.OnTrainAttempted += SetSelected;
            troopCosts = new Dictionary<string, Model.JsonCost>()
            {
                { "Archer", Model.TroopBase.TroopProperties.Archer.Cost },
                { "Boat", Model.TroopBase.TroopProperties.Boat.Cost },
                { "Builder", Model.TroopBase.TroopProperties.Builder.Cost },
                { "Catapult", Model.TroopBase.TroopProperties.Catapult.Cost },
                { "Scout", Model.TroopBase.TroopProperties.Scout.Cost },
                { "Settler", Model.TroopBase.TroopProperties.Settler.Cost },
                { "Warrior", Model.TroopBase.TroopProperties.Warrior.Cost }
            };
        }

        private void UpdateContent(Model.Player player)
        {
            dropdown.ClearOptions();
            IList<string> availableTroops = player.AvailableTroops;
            foreach (string troopName in availableTroops)
            {
                if (player.ResourceContainer.HasEnoughFor(JsonToModelCost(troopCosts[troopName])))
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

        private Model.Cost JsonToModelCost(Model.JsonCost jsonCost)
        {
            return new Model.Cost(jsonCost.Money, jsonCost.Material, jsonCost.Food);
        }
    }
}