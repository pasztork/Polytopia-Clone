using UnityEngine;

namespace View
{
    public class ArcheryTechView : TechTreeItem
    {
        private ArcheryTechView()
        {
            startColor = new Color(1, 1, 0, 1);
            Description = "Unlocks Archer troop";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.ArcheryTech(Name, new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost), Description);
            return techItem;
        }
    }
}