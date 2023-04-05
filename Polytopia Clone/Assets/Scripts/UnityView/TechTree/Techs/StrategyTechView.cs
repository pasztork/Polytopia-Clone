using UnityEngine;

namespace View
{
    public class StrategyTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.2f, 0, 1);
            Description = "Increase troop dodge rate";
            Cost = Model.TechTreeItemBase.ItemCosts["Strategy"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.StrategyTech();
            return techItem;
        }
    }
}