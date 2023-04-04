using UnityEngine;

namespace View
{
    public class MiningTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "Can build Supplier on Mountain";
            Cost = Model.TechTreeItemBase.ItemCosts["Mining"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.MiningTech();
            return techItem;
        }
    }
}