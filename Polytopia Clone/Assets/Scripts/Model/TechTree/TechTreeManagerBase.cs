using System;
using System.Collections.Generic;

namespace Model
{
    public abstract class TechTreeManagerBase
    {
        public event Action<Player> OnTechUnlocked;

        public abstract IList<TechTreeItemBase> GetTechsOfCurrentPlayer();

        public abstract bool UnlockTech(TechTreeItemBase tech);

        public abstract Dictionary<string, TechTreeItemBase> ConnectTree(Dictionary<string, TechTreeItemBase> items);

        protected void RaiseOnTechUnlocked(Player player)
        {
            OnTechUnlocked?.Invoke(player);
        }
    }
}
