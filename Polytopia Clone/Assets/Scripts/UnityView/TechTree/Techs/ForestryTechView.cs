using UnityEngine;

namespace View
{
    public class ForestryTechView : TechTreeItem
    {
        private ForestryTechView()
        {
            startColor = new Color(1, 1, 0, 1);
            Description = "Can build Supplier on Forest";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.ForestryTech(new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost));
            return techItem;
        }
    }
}