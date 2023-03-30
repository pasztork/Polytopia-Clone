using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public abstract class TechTreeItem : MonoBehaviour
    {
        [SerializeField] protected View.Cost cost;
        public View.Cost Cost { get => cost; set => cost = value; }
        public string Name { get => GetComponentInChildren<TextMeshProUGUI>().text; }
        public string Description { get; protected set; }
        public bool IsUnlocked { get; set; }

        protected Color startColor;
        private Color selectColor = Color.magenta;
        private Color unlockColor = Color.green;

        public abstract Model.TechTreeItemBase ToModel();

        public void OnItemClicked()
        {
            View.TechTreeManager.Instance.ShowTechTreeItemInfo(this);
            if (GetComponent<Image>().color == startColor)
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
            GetComponent<Image>().color = startColor;
            GetComponent<Button>().interactable = false;
        }

        public void ItemAvailable()
        {
            GetComponent<Image>().color = startColor;
            GetComponent<Button>().interactable = true;
        }

        public void ItemUnlocked()
        {
            GetComponent<Image>().color = unlockColor;
            GetComponent<Button>().interactable = false;
        }

        public void ResetColor()
        {
            GetComponent<Image>().color = startColor;
        }
    }
}
