using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class TechTreeItem : MonoBehaviour
    {
        [SerializeField] protected Controller.Cost cost;
        public Controller.Cost Cost { get => cost; set => cost = value; }
        public string Name { get; set; }
        public bool IsUnlocked { get; set; }

        private Color startColor;
        private Color selectColor = Color.magenta;
        private Color unlockColor = Color.green;

        private void Awake()
        {
            startColor = GetComponent<Image>().color;
            Name = GetComponentInChildren<TextMeshProUGUI>().text;
        }

        public void OnItemClicked()
        {
            Controller.TechTreeManager.Instance.ShowTechTreeItemInfo(this);
            if(GetComponent<Image>().color == startColor)
            {
                GetComponent<Image>().color = selectColor;

            }
            else
            {
                GetComponent<Image>().color = startColor;
            }
        }

        public void ItemUnlocked()
        {
            GetComponent<Image>().color = unlockColor;
            enabled = false;
        }

        public Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem = new Model.TechTreeItemBase();
            techItem.TechTreeItemProperty = new Model.TechTreeItemProperty(name, new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost));
            if(GetComponent<Image>().color == unlockColor)
            {
                techItem.TechTreeItemProperty.IsUnlocked = true;
            }
            return techItem;
        }
    }
}
