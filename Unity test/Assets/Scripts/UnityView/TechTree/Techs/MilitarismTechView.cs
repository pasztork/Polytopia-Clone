using UnityEngine;

namespace View
{
    public class MilitarismTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.4f, 0, 1);
            Description = "Increase troop damage";
            Cost = Model.TechTreeItemBase.ItemCosts["Militarism"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.MilitarismTech();
            return techItem;
        }
    }
}