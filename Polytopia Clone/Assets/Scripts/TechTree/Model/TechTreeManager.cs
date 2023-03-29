using System.Collections.Generic;

namespace Model
{
    public class TechTreeManager : TechTreeManagerBase
    {
        public override IList<TechTreeItemBase> GetTechsOfCurrentPlayer()
        {
            return DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Techs;
        }

        public override bool UnlockTech(TechTreeItemBase tech)
        {
            bool learnt = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.UnlockTech(tech);
            if (learnt)
            {
                RaiseOnTechUnlocked(DependencyContainer.Get<TurnManagerBase>().CurrentPlayer);
            }
            return learnt;
        }

        public override IList<TechTreeItemBase> ConnectTree(IList<TechTreeItemBase> items)
        {
            items[2].Requirements.Add(items[9]);
            items[2].Requirements.Add(items[10]);
            items[5].Requirements.Add(items[4]);
            items[6].Requirements.Add(items[1]);
            items[7].Requirements.Add(items[3]);
            items[7].Requirements.Add(items[5]);
            items[7].Requirements.Add(items[11]);
            items[8].Requirements.Add(items[3]);
            items[9].Requirements.Add(items[1]);
            items[10].Requirements.Add(items[3]);
            items[10].Requirements.Add(items[13]);
            items[11].Requirements.Add(items[4]);
            items[12].Requirements.Add(items[14]);
            items[13].Requirements.Add(items[0]);
            items[14].Requirements.Add(items[4]);
            items[14].Requirements.Add(items[6]);
            items[15].Requirements.Add(items[10]);
            items[16].Requirements.Add(items[9]);
            items[17].Requirements.Add(items[10]);
            return items;
        }
    }
}
