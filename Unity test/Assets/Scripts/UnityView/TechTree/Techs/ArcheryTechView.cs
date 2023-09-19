using UnityEngine;

namespace View
{
    public class ArcheryTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 1, 0, 1);
            Description = "Unlocks Archer troop";
            Cost = Model.TechTreeItemBase.ItemCosts["Archery"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.ArcheryTech();
            return techItem;
        }
    }
}