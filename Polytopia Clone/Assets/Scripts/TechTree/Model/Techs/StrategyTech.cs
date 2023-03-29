namespace Model
{
    public class StrategyTech : TechTreeItemBase
    {
        public StrategyTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect()
        {
            //increases dodge rate
        }
    }
}
