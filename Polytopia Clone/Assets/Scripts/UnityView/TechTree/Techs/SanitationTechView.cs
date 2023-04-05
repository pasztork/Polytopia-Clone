using UnityEngine;

namespace View
{
    public class SanitationTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.2f, 0, 1);
            Description = "Troops can heal";
            Cost = Model.TechTreeItemBase.ItemCosts["Sanitation"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.SanitationTech();
            return techItem;
        }
    }
}