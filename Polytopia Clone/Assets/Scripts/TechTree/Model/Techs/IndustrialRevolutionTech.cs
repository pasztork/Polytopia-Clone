namespace Model
{
    public class IndustrialRevolutionTech : TechTreeItemBase
    {
        public IndustrialRevolutionTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect()
        {
            //increases suppliers production rate
        }
    }
}
