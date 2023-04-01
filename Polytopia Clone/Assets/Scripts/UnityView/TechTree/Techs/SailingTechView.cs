using UnityEngine;

namespace View
{
    public class SailingTechView : TechTreeItem
    {
        private SailingTechView()
        {
            startColor = new Color(1, 0.4f, 0, 1);
            Description = "Unlock Boat troop";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.SailingTech(new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost));
            return techItem;
        }
    }
}