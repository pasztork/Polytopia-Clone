using UnityEngine;

namespace View
{
    public class IrrigationTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "Increase Farm produce rate";
            Cost = Model.TechTreeItemBase.ItemCosts["Irrigation"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.IrrigationTech();
            return techItem;
        }
    }
}