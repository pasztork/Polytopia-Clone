using UnityEngine;

namespace View
{
    public class SanitationTechView : TechTreeItem
    {
        private SanitationTechView()
        {
            startColor = new Color(1, 0.2f, 0, 1);
            Description = "Troops can heal";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.SanitationTech(new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost));
            return techItem;
        }
    }
}