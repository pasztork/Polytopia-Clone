namespace Model
{
    public class StrategyTech : TechTreeItemBase
    {
        public StrategyTech(string name, Cost cost, string description) : base(name, cost, description) 
        {
            HashCode = "Strategy";
        }

        public override void ActivateEffect(Player player)
        {
            //increases dodge rate
        }
    }
}
