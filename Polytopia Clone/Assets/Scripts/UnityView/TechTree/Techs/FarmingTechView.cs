using UnityEngine;

namespace View
{
    public class FarmingTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 1, 0, 1);
            Description = "Unlocks Farm building";
            Cost = Model.TechTreeItemBase.ItemCosts["Farming"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.FarmingTech();
            return techItem;
        }
    }
}