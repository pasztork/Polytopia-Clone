using UnityEngine;

namespace View
{
    public class MathematicsTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "Reduce building cost";
            Cost = Model.TechTreeItemBase.ItemCosts["Mathematics"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.MathematicsTech();
            return techItem;
        }
    }
}