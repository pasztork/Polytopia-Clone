using UnityEngine;

namespace View
{
    public class HarborTechView : TechTreeItem
    {
        private HarborTechView()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "Unlocks Harbor building";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.HarborTech(new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost));
            return techItem;
        }
    }
}