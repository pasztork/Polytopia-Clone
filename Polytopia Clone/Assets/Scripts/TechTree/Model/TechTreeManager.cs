using System.Collections.Generic;

namespace Model
{
    public class TechTreeManager : TechTreeManagerBase
    {
        public override IList<TechTreeItemBase> GetTechsOfCurrentPlayer()
        {
            if (DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Techs == null)
            {
                DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Techs = TechTreeModel;
                return TechTreeModel;
            }
            return DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Techs;
        }

        public override bool UnlockTech(TechTreeItemBase tech)
        {
            if (tech == null)
                return false;

            bool learnt = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.UnlockTech(tech);
            if (learnt)
            {
                RaiseOnTechUnlocked(DependencyContainer.Get<TurnManagerBase>().CurrentPlayer);
            }
            return learnt;
        }

        public override void BuildTechTree(IList<TechTreeItemBase> items)
        {
            TechTreeModel = new List<TechTreeItemBase>();
        }
    }
}
