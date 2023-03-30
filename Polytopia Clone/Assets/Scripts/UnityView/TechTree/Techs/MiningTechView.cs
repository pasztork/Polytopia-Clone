using UnityEngine;

namespace View
{
    public class MiningTechView : TechTreeItem
    {
        private MiningTechView()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "Can build Supplier on Mountain";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.MiningTech(Name, new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost), Description);
            return techItem;
        }
    }
}