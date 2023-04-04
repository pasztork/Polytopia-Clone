using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace View
{
    public class TechTreeManager : MonoBehaviour
    {
        private static TechTreeManager instance;
        public static TechTreeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<TechTreeManager>();
                }
                return instance;
            }
        }

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
        [SerializeField] private TextMeshProUGUI itemDescriptionText;

        private void Start()
        {
            Model.GameManager.Get<Model.TurnManagerBase>().OnTurnStarted += EndTurn;
        }

        public void OnTechTreeButtonClick()
        {
            var modelTechs = Controller.GameManager.Get<Controller.TechTreeManagerBase>().GetTechsOfCurrentPlayer();
            if (modelTechs == null)
                return;

            UpdateTechElements(modelTechs);
            techTreeWindow.SetActive(!techTreeWindow.activeSelf);
        }

        private void UpdateTechElements(IList<Model.TechTreeItemBase> modelTechs)
        {
            for (int i = 0; i < modelTechs.Count; i++)
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
            if (SelectedTechTreeItem != null)
            {
                SelectedTechTreeItem.ResetColor();
            }

            SelectedTechTreeItem = item;
            itemNameText.text = item.Name;
            itemMoneyCostText.text = $"Money Cost: {item.Cost.Money}";
            itemMaterialCostText.text = $"Material Cost: {item.Cost.Material}";
            itemFoodCostText.text = $"Food Cost: {item.Cost.Food}";
            itemDescriptionText.text = item.Description;
        }

        public void OnLearnTechButtonClick()
        {
            if (SelectedTechTreeItem == null)
                return;

            bool learnt = Controller.GameManager.Get<Controller.TechTreeManagerBase>().UnlockTech(SelectedTechTreeItem.ToModel());
            if (!learnt)
                return;

            SelectedTechTreeItem.ItemUnlocked();
            SelectedTechTreeItem = null;
            var modelTechs = Controller.GameManager.Get<Controller.TechTreeManagerBase>().GetTechsOfCurrentPlayer();
            UpdateTechElements(modelTechs);
        }

        private void EndTurn(Model.Player player)
        {
            if (SelectedTechTreeItem != null)
            {
                SelectedTechTreeItem.ResetColor();
                SelectedTechTreeItem = null;
            }
            TechTreeWindow.SetActive(false);
        }

        // ez a modell feladata
        // kiad egy esemenyt, hogy felepitette
        public Dictionary<string, Model.TechTreeItemBase> GetNewTechTree()
        {
            Dictionary<string, Model.TechTreeItemBase> modelItems = new Dictionary<string, Model.TechTreeItemBase>();
            foreach (var viewItem in TechTreeItems)
            {
                var modelItem = viewItem.ToModel();
                modelItems[modelItem.HashCode] = modelItem;
            }
            return Model.GameManager.Get<Model.TechTreeManagerBase>().ConnectTree(modelItems);
        }
    }
}
