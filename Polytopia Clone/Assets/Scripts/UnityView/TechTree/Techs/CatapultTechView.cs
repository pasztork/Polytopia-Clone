using UnityEngine;

namespace View
{
    public class CatapultTechView : TechTreeItem
    {
        private CatapultTechView()
        {
            startColor = new Color(1, 0.2f, 0, 1);
            Description = "Unlocks Catapult troop";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.CatapultTech(Name, new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost), Description);
            return techItem;
        }
    }
}