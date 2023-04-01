namespace Model
{
    public class StockMarketTech : TechTreeItemBase
    {
        private int moneyProductionBoost = 10;
        public StockMarketTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "StockMarket";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.BankProductionBonus += moneyProductionBoost;
            foreach (BuildingBase building in player.Buildings)
            {
                building.IncreaseMoneyProduction(moneyProductionBoost);
            }
        }
    }
}
