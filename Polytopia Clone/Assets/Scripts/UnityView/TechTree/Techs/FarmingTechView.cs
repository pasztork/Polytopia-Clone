using UnityEngine;

namespace View
{
    public class FarmingTechView : TechTreeItem
    {
        private FarmingTechView()
        {
            startColor = new Color(1, 1, 0, 1);
            Description = "Unlocks Farm building";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.FarmingTech(Name, new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost), Description);
            return techItem;
        }
    }
}