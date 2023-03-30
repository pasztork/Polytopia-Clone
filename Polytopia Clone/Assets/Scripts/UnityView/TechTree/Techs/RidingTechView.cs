using UnityEngine;

namespace View
{
    public class RidingTechView : TechTreeItem
    {
        private RidingTechView()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "+1 move range to land troops";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.RidingTech(Name, new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost), Description);
            return techItem;
        }
    }
}