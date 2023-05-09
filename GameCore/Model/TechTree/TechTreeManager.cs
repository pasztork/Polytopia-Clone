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
            if (items.Count == 0) { FillTechDict(items); }

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

        private void FillTechDict(Dictionary<string, TechTreeItemBase> dict)
        {
            dict.Add("Archery", new ArcheryTech());
            dict.Add("Banking", new BankingTech());
            dict.Add("Catapult", new CatapultTech());
            dict.Add("Farming", new FarmingTech());
            dict.Add("Forestry", new ForestryTech());
            dict.Add("GemMining", new GemMiningTech());
            dict.Add("Harbor", new HarborTech());
            dict.Add("IndustrialRevolution", new IndustrialRevolutionTech());
            dict.Add("Irrigation", new IrrigationTech());
            dict.Add("Mathematics", new MathematicsTech());
            dict.Add("Militarism", new MilitarismTech());
            dict.Add("Mining", new MiningTech());
            dict.Add("Navigation", new NavigationTech());
            dict.Add("Riding", new RidingTech());
            dict.Add("Sailing", new SailingTech());
            dict.Add("Sanitation", new SanitationTech());
            dict.Add("StockMarket", new StockMarketTech());
            dict.Add("Strategy", new StrategyTech());
        }
    }
}
