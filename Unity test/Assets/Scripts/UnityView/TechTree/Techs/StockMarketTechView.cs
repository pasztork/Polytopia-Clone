using UnityEngine;

namespace View
{
    public class StockMarketTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.4f, 0, 1);
            Description = "Increase Bank produce rate";
            Cost = Model.TechTreeItemBase.ItemCosts["StockMarket"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.StockMarketTech();
            return techItem;
        }
    }
}