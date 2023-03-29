namespace Model
{
    public class IrrigationTech : TechTreeItemBase
    {
        public IrrigationTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //increases Farm production rate
        }
    }
}
