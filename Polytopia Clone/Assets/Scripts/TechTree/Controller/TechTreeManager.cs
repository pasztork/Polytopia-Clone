using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Controller
{
    public class TechTreeManager : MonoBehaviour
    {
        public static TechTreeManager Instance { get; private set; }

        [SerializeField] private IList<View.TechTreeItem> items = new List<View.TechTreeItem>();
        public IList<View.TechTreeItem> Items { get => items; private set => items = value; }

        //public Dictionary<View.TechTreeItem, Model.TechTreeItemBase> ViewToModelMap { get; }
        //    = new Dictionary<View.TechTreeItem, Model.TechTreeItemBase>();

        //public Dictionary<Model.TechTreeItemBase, View.TechTreeItem> ModelToViewMap { get; }
        //    = new Dictionary<Model.TechTreeItemBase, View.TechTreeItem>();

        public View.TechTreeItem SelectedTechTreeItem { get; private set; }

        [Header("TechTree Elements")]
        [SerializeField] private GameObject techTreeWindow;
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
