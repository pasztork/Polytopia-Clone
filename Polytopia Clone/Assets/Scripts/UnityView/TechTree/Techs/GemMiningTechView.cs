using UnityEngine;

namespace View
{
    public class GemMiningTechView : TechTreeItem
    {
        private void Awake()
        {
            startColor = new Color(1, 0.8f, 0, 1);
            Description = "Can build Supplier on Sand";
            Cost = Model.TechTreeItemBase.ItemCosts["GemMining"];
        }

        public override Model.TechTreeItemBase ToModel()
        {
            Model.TechTreeItemBase techItem =
                new Model.GemMiningTech();
            return techItem;
        }
    }
}