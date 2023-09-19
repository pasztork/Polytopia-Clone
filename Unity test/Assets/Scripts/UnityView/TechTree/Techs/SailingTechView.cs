using UnityEngine;

namespace View
{
    public class SailingTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.4f, 0, 1);
            Description = "Unlock Boat troop";
            Cost = Model.TechTreeItemBase.ItemCosts["Riding"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.SailingTech();
            return techItem;
        }
    }
}