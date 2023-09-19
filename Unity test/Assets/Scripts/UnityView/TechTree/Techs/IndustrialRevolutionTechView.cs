using UnityEngine;

namespace View
{
    public class IndustrialRevolutionTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.4f, 0, 1);
            Description = "Increase Supplier produce rate";
            Cost = Model.TechTreeItemBase.ItemCosts["IndustrialRevolution"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.IndustrialRevolutionTech();
            return techItem;
        }
    }
}