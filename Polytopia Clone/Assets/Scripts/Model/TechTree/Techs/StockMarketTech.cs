namespace Model
{
    public class StockMarketTech : TechTreeItemBase
    {
        private readonly int moneyProductionBoost = 10;
        public StockMarketTech(Cost cost) : base(cost)
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
