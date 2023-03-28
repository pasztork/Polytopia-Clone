using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Controller
{
    public class TechTreeManager : MonoBehaviour
    {
        public static TechTreeManager Instance { get; private set; }

        [SerializeField] private List<View.TechTreeItem> techTreeItems = new List<View.TechTreeItem>();
        public List<View.TechTreeItem> TechTreeItems { get => techTreeItems; private set => techTreeItems = value; }

        public View.TechTreeItem SelectedTechTreeItem { get; private set; }

        [Header("TechTree Elements")]
        [SerializeField] private GameObject techTreeWindow;
        public GameObject TechTreeWindow { get => techTreeWindow; private set => techTreeWindow = value; }
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemMoneyCostText;
        [SerializeField] private TextMeshProUGUI itemMaterialCostText;
        [SerializeField] private TextMeshProUGUI itemFoodCostText;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one TechTreeManager in scene!");
                return;
            }
            Instance = this;
        }

        public void OnTechTreeButtonClick()
        {
            techTreeWindow.SetActive(!techTreeWindow.activeSelf);
        }

        public void ShowTechTreeItemInfo(View.TechTreeItem item)
        {
            SelectedTechTreeItem = item;
            itemNameText.text = item.Name;
            itemMoneyCostText.text = $"Money Cost: {item.Cost.MoneyCost}";
            itemMaterialCostText.text = $"Material Cost: {item.Cost.MaterialCost}";
            itemFoodCostText.text = $"Food Cost: {item.Cost.FoodCost}";
        }
    }
}
