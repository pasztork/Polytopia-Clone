namespace Model
{
    public class IrrigationTech : TechTreeItemBase
    {
        public IrrigationTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect()
        {
            //increases Farm production rate
        }
    }
}
