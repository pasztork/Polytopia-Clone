using UnityEngine;

namespace View
{
    public class StockMarketTechView : TechTreeItem
    {
        private StockMarketTechView()
        {
            startColor = new Color(1, 0.4f, 0, 1);
            Description = "Increase Bank produce rate";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.StockMarketTech(new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost));
            return techItem;
        }
    }
}