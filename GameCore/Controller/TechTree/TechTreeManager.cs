using System.Collections.Generic;

namespace Controller
{
    public class TechTreeManager : TechTreeManagerBase
    {
        public override IList<Model.TechTreeItemBase> GetTechsOfCurrentPlayer()
        {
            return Model.GameManager.Get<Model.TechTreeManagerBase>().GetTechsOfCurrentPlayer();
        }

        public override bool UnlockTech(Model.TechTreeItemBase techTreeItem)
        {
            return Model.GameManager.Get<Model.TechTreeManagerBase>().UnlockTech(techTreeItem);
        }
    }
}
