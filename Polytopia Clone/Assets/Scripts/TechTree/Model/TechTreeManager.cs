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
                if(TurnManager.Instance.CurrentPlayer.Techs == null)
                {
                    TurnManager.Instance.CurrentPlayer.Techs = TechTreeModel;
                    return TechTreeModel;
                }
                else
                {
                    return TurnManager.Instance.CurrentPlayer.Techs;
                }
            }
        }

        public event Action<Player> OnTechUnlocked;

        public bool UnlockTech(TechTreeItemBase tech)
        {
            if (tech == null)
                return false;

            bool learnt = TurnManager.Instance.CurrentPlayer.UnlockTech(tech);
            if (learnt)
            {
                OnTechUnlocked?.Invoke(TurnManager.Instance.CurrentPlayer);
            }
            return learnt;
        }

        public void BuildTechTree()
        {
            TechTreeModel = new List<TechTreeItemBase>();
        }
    }
}
