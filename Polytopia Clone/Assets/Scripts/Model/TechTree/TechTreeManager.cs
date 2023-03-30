using System.Collections.Generic;

namespace Model
{
    public class TechTreeManager : TechTreeManagerBase
    {
        public override IList<TechTreeItemBase> GetTechsOfCurrentPlayer()
        {
            var playerTechs = GameManager.Get<TurnManagerBase>().CurrentPlayer.Techs;
            IList<TechTreeItemBase> techValues = new List<TechTreeItemBase>();
            foreach (var tech in playerTechs.Values) 
            {
                techValues.Add(tech);
            }
            return techValues;
        }

        public override bool UnlockTech(TechTreeItemBase tech)
        {
            bool learnt = GameManager.Get<TurnManagerBase>().CurrentPlayer.UnlockTech(tech);
            if (learnt)
            {
                RaiseOnTechUnlocked(GameManager.Get<TurnManagerBase>().CurrentPlayer);
            }
            return learnt;
        }

        public override Dictionary<string, TechTreeItemBase> ConnectTree(Dictionary<string, TechTreeItemBase> items)
        {
            items["Catapult"].Requirements.Add(items["Mathematics"]);
            items["Catapult"].Requirements.Add(items["Militarism"]);
            items["GemMining"].Requirements.Add(items["Forestry"]);
            items["Harbor"].Requirements.Add(items["Banking"]);
            items["IndustrialRevolution"].Requirements.Add(items["Farming"]);
            items["IndustrialRevolution"].Requirements.Add(items["GemMining"]);
            items["IndustrialRevolution"].Requirements.Add(items["Mining"]);
            items["Irrigation"].Requirements.Add(items["Farming"]);
            items["Mathematics"].Requirements.Add(items["Banking"]);
            items["Militarism"].Requirements.Add(items["Farming"]);
            items["Militarism"].Requirements.Add(items["Riding"]);
            items["Mining"].Requirements.Add(items["Forestry"]);
            items["Navigation"].Requirements.Add(items["Sailing"]);
            items["Riding"].Requirements.Add(items["Archery"]);
            items["Sailing"].Requirements.Add(items["Forestry"]);
            items["Sailing"].Requirements.Add(items["Harbor"]);
            items["Sanitation"].Requirements.Add(items["Militarism"]);
            items["StockMarket"].Requirements.Add(items["Mathematics"]);
            items["Strategy"].Requirements.Add(items["Militarism"]);
            return items;
        }
    }
}
