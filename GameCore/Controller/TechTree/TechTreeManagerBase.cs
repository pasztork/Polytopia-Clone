using System.Collections.Generic;

namespace Controller
{
    public abstract class TechTreeManagerBase
    {
        public abstract IList<Model.TechTreeItemBase> GetTechsOfCurrentPlayer();

        public abstract bool UnlockTech(Model.TechTreeItemBase techTreeItem);
    }
}
