using UnityEngine;

namespace View
{
    public class BankingTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 1, 0, 1);
            Description = "Unlocks Bank building";
            Cost = Model.TechTreeItemBase.ItemCosts["Banking"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.BankingTech();
            return techItem;
        }
    }
}