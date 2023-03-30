using UnityEngine;

namespace View
{
    public class IrrigationTechView : TechTreeItem
    {
        private IrrigationTechView()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "Increase Farm produce rate";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.IrrigationTech(Name, new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost), Description);
            return techItem;
        }
    }
}