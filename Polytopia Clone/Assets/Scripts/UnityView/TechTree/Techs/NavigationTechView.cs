using UnityEngine;

namespace View
{
    public class NavigationTechView : TechTreeItem
    {
        private NavigationTechView()
        {
            startColor = new Color(1, 0.2f, 0, 1);
            Description = "+1 move range to water troops";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.NavigationTech(new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost));
            return techItem;
        }
    }
}