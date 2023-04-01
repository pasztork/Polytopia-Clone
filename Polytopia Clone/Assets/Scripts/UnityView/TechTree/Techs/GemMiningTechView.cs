using UnityEngine;

namespace View
{
    public class GemMiningTechView : TechTreeItem
    {
        private GemMiningTechView()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "Can build Supplier on Sand";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.GemMiningTech(new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost));
            return techItem;
        }
    }
}