using UnityEngine;

namespace View
{
    public class NavigationTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.2f, 0, 1);
            Description = "+1 move range to water troops";
            Cost = Model.TechTreeItemBase.ItemCosts["Navigation"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.NavigationTech();
            return techItem;
        }
    }
}