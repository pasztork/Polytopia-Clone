using Model;

namespace Util
{
    public class TechTreeItemFactory : GameObjectFactory<TechTreeItemBase>
    {
        public TechTreeItemFactory() : base()
        {
            StringToFuncDictionary.Add("Archery", Factory.Create<ArcheryTech>);
            StringToFuncDictionary.Add("Banking", Factory.Create<BankingTech>);
            StringToFuncDictionary.Add("Catapult", Factory.Create<CatapultTech>);
            StringToFuncDictionary.Add("Farming", Factory.Create<FarmingTech>);
            StringToFuncDictionary.Add("Forestry", Factory.Create<ForestryTech>);
            StringToFuncDictionary.Add("GemMining", Factory.Create<GemMiningTech>);
            StringToFuncDictionary.Add("Harbor", Factory.Create<HarborTech>);
            StringToFuncDictionary.Add("IndustrialRevolution", Factory.Create<IndustrialRevolutionTech>);
            StringToFuncDictionary.Add("Irrigation", Factory.Create<IrrigationTech>);
            StringToFuncDictionary.Add("Mathematics", Factory.Create<MathematicsTech>);
            StringToFuncDictionary.Add("Militarism", Factory.Create<MilitarismTech>);
            StringToFuncDictionary.Add("Mining", Factory.Create<MiningTech>);
            StringToFuncDictionary.Add("Navigation", Factory.Create<NavigationTech>);
            StringToFuncDictionary.Add("Riding", Factory.Create<RidingTech>);
            StringToFuncDictionary.Add("Sailing", Factory.Create<SailingTech>);
            StringToFuncDictionary.Add("Sanitation", Factory.Create<SanitationTech>);
            StringToFuncDictionary.Add("StockMarket", Factory.Create<StockMarketTech>);
            StringToFuncDictionary.Add("Strategy", Factory.Create<StrategyTech>);
        }
    }
}
