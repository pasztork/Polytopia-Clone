using UnityEngine;

namespace View
{
    public class BankingTechView : TechTreeItem
    {
        private BankingTechView()
        {
            startColor = new Color(1, 1, 0, 1);
            Description = "Unlocks Bank building";
        }
        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.BankingTech(new Model.Cost(Cost.MoneyCost, Cost.MaterialCost, Cost.FoodCost));
            return techItem;
        }
    }
}