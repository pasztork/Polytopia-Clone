using UnityEngine;

namespace View
{
    public class RidingTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "+1 move range to land troops";
            Cost = Model.TechTreeItemBase.ItemCosts["Riding"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.RidingTech();
            return techItem;
        }
    }
}