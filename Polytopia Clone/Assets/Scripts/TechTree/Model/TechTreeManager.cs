using System;
using System.Collections.Generic;

namespace Model
{
    public class TechTreeManager
    {
        private static TechTreeManager instance;
        public static TechTreeManager Instance
        {
            get
            {
                instance ??= new TechTreeManager();
                return instance;
            }
        }

        public IList<TechTreeItemBase> TechTreeModel { get; private set; }

        public IList<TechTreeItemBase> TechsOfCurrentPlayer
        {
            get
            {
                if (DependencyContainer.Get<TurnManager>().CurrentPlayer.Techs == null)
                {
                    DependencyContainer.Get<TurnManager>().CurrentPlayer.Techs = TechTreeModel;
                    return TechTreeModel;
                }
                return DependencyContainer.Get<TurnManager>().CurrentPlayer.Techs;
            }
        }

        public event Action<Player> OnTechUnlocked;

        public bool UnlockTech(TechTreeItemBase tech)
        {
            if (tech == null)
                return false;

            bool learnt = DependencyContainer.Get<TurnManager>().CurrentPlayer.UnlockTech(tech);
            if (learnt)
            {
                OnTechUnlocked?.Invoke(DependencyContainer.Get<TurnManager>().CurrentPlayer);
            }
            return learnt;
        }

        public void BuildTechTree()
        {
            TechTreeModel = new List<TechTreeItemBase>();
        }
    }
}
