using System;
using System.Collections.Generic;

namespace Model
{
    public abstract class TechTreeManagerBase
    {
        public event Action<Player> OnTechUnlocked;

        public IList<TechTreeItemBase> TechTreeModel { get; protected set; }

        public abstract IList<TechTreeItemBase> GetTechsOfCurrentPlayer();

        public abstract bool UnlockTech(TechTreeItemBase tech);

        public abstract void BuildTechTree();

        protected void RaiseOnTechUnlocked(Player player)
        {
            OnTechUnlocked?.Invoke(player);
        }
    }
}
