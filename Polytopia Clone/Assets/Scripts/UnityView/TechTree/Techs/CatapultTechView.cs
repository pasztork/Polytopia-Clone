using UnityEngine;

namespace View
{
    public class CatapultTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.2f, 0, 1);
            Description = "Unlocks Catapult troop";
            Cost = Model.TechTreeItemBase.ItemCosts["Catapult"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.CatapultTech();
            return techItem;
        }
    }
}