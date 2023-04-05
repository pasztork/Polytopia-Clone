using UnityEngine;

namespace View
{
    public class HarborTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "Unlocks Harbor building";
            Cost = Model.TechTreeItemBase.ItemCosts["Harbor"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.HarborTech();
            return techItem;
        }
    }
}