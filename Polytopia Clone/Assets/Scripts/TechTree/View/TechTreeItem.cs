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
        public string Description { get; set; }
        public bool IsUnlocked { get; set; }

        [SerializeField] private Color startColor;
        private Color selectColor = Color.magenta;
        private Color unlockColor = Color.green;

        private void Start()
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

        public void ItemLocked()
        {
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().color = startColor;
        }

        public void ItemAvailable()
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Image>().color = startColor;
        }

        public void ItemUnlocked()
        {
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().color = unlockColor;
        }

        public void ResetColor()
        {
            GetComponent<Image>().color = startColor;
        }

        public Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem = new Model.TechTreeItemBase
            {
                TechTreeItemProperty = new Model.TechTreeItemProperty(Name, new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost), Description)
            };
            if (GetComponent<Image>().color == unlockColor)
            {
                techItem.TechTreeItemProperty.IsUnlocked = true;
            }
            return techItem;
        }
    }
}
