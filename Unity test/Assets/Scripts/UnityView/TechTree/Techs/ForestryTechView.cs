using UnityEngine;

namespace View
{
    public class ForestryTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 1, 0, 1);
            Description = "Can build Supplier on Forest";
            Cost = Model.TechTreeItemBase.ItemCosts["Forestry"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.ForestryTech();
            return techItem;
        }
    }
}