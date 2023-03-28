using System;
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
            var modelTechs = Model.TechTreeManager.Instance.TechsOfCurrentPlayer;
            if (modelTechs == null)
                return;

            UpdateTechElements(modelTechs);
            techTreeWindow.SetActive(!techTreeWindow.activeSelf);
        }

        private void UpdateTechElements(IList<Model.TechTreeItemBase> modelTechs)
        {
            for(int i = 0; i < modelTechs.Count; i++)
            {
                if (modelTechs[i].TechTreeItemProperty.IsUnlocked)
                {
                    TechTreeItems[i].ItemUnlocked();
                }
                else if (modelTechs[i].IsAvailable)
                {
                    TechTreeItems[i].ItemAvailable();
                }
                else
                {
                    TechTreeItems[i].ItemLocked();
                }
            }
        }

        public void ShowTechTreeItemInfo(View.TechTreeItem item)
        {
            if(SelectedTechTreeItem != null)
            {
                SelectedTechTreeItem.ResetColor();
            }

            SelectedTechTreeItem = item;
            itemNameText.text = item.Name;
            itemMoneyCostText.text = $"Money Cost: {item.Cost.MoneyCost}";
            itemMaterialCostText.text = $"Material Cost: {item.Cost.MaterialCost}";
            itemFoodCostText.text = $"Food Cost: {item.Cost.FoodCost}";
        }

        public void OnLearnTechButtonClick()
        {
            if(SelectedTechTreeItem == null)
                return;

            bool learnt = Model.TechTreeManager.Instance.UnlockTech(SelectedTechTreeItem.ToModel());
            if (!learnt)
                return;

            SelectedTechTreeItem.ItemUnlocked();
            SelectedTechTreeItem = null;
        }
    }
}
