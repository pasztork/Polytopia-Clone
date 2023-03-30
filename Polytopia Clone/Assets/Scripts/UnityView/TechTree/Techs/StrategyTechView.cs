using UnityEngine;

namespace View
{
    public class StrategyTechView : TechTreeItem
    {
        private StrategyTechView()
        {
            startColor = new Color(1, 0.2f, 0, 1);
            Description = "Increase troop dodge rate";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.StrategyTech(Name, new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost), Description);
            return techItem;
        }
    }
}