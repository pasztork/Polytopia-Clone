using UnityEngine;

namespace View
{
    public class IndustrialRevolutionTechView : TechTreeItem
    {
        private IndustrialRevolutionTechView()
        {
            startColor = new Color(1, 0.4f, 0, 1);
            Description = "Increase Supplier produce rate";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.IndustrialRevolutionTech(Name, new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost), Description);
            return techItem;
        }
    }
}