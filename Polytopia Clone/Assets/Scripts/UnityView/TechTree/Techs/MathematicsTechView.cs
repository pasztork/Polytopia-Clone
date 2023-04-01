using UnityEngine;

namespace View
{
    public class MathematicsTechView : TechTreeItem
    {
        private MathematicsTechView()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "Reduce building cost";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.MathematicsTech(new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost));
            return techItem;
        }
    }
}