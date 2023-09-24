using TMPro;
using UnityEngine;

namespace View
{
    public class InfoPanel : MonoBehaviour
    {
        public static InfoPanel Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI materialText;
        [SerializeField] private TextMeshProUGUI foodText;
        [SerializeField] private TextMeshProUGUI nameText;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one InfoPanel in scene!");
                return;
            }
            Instance = this;
            Model.GameManager.Get<Model.TurnManagerBase>().OnTurnStarted += UpdateContent;
            Model.GameManager.Get<Model.BuildManagerBase>().OnBuildingBuilt += UpdateContent;
            Model.GameManager.Get<Model.TechTreeManagerBase>().OnTechUnlocked += UpdateContent;
            Model.GameManager.Get<Model.TrainManagerBase>().OnTroopTrained += UpdateContent;
        }

        private void Start()
        {
        }

        public void UpdateContent(Model.Player player)
        {
            nameText.text = player.Name;
            Model.ResourceContainer resourceContainer = player.ResourceContainer;
            moneyText.text = $"Money: {resourceContainer.MoneyCount}";
            materialText.text = $"Material: {resourceContainer.MaterialCount}";
            foodText.text = $"Food: {resourceContainer.FoodCount}";
        }
    }
}