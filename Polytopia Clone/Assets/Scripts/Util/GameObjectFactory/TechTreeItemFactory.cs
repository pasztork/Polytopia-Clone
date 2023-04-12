using Model;
using System;
using System.Collections.Generic;

namespace Util
{
    public class TechTreeItemFactory : IGameObjectFactory<TechTreeItemBase>
    {
        private readonly IDictionary<string, Type> _stringToTypeDictionary = new Dictionary<string, Type>
        {
            { "Archery", typeof(ArcheryTech) },
            { "Banking", typeof(BankingTech) },
            { "Catapult", typeof(CatapultTech) },
            { "Farming", typeof(FarmingTech) },
            { "Forestry", typeof(ForestryTech) },
            { "GemMining", typeof(GemMiningTech) },
            { "Harbor", typeof(HarborTech) },
            { "IndustrialRevolution", typeof(IndustrialRevolutionTech) },
            { "Irrigation", typeof(IrrigationTech) },
            { "Mathematics", typeof(MathematicsTech) },
            { "Militarism", typeof(MilitarismTech) },
            { "Mining", typeof(MiningTech) },
            { "Navigation", typeof(NavigationTech) },
            { "Riding", typeof(RidingTech) },
            { "Sailing", typeof(SailingTech) },
            { "Sanitation", typeof(SanitationTech) },
            { "StockMarket", typeof(StockMarketTech) },
            { "Strategy", typeof(StrategyTech) },
        };

        public TechTreeItemBase Instantiate(string typeName)
        {
            if (!_stringToTypeDictionary.ContainsKey(typeName))
            {
                throw new ArgumentOutOfRangeException($"No such tech exists: {typeName}");
            }

            Type troopType = _stringToTypeDictionary[typeName];
            return Activator.CreateInstance(troopType) as TechTreeItemBase;
        }
    }
}
