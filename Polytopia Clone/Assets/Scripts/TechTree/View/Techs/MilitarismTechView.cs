using UnityEngine;

namespace View
{
    public class MilitarismTechView : TechTreeItem
    {
        private MilitarismTechView()
        {
            startColor = new Color(1, 0.4f, 0, 1);
            Description = "Increase troop damage";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.MilitarismTech(Name, new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost), Description);
            return techItem;
        }
    }
}