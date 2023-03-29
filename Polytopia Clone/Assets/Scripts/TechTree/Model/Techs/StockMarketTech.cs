namespace Model
{
    public class StockMarketTech : TechTreeItemBase
    {
        public StockMarketTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //increases bank production rate
        }
    }
}
