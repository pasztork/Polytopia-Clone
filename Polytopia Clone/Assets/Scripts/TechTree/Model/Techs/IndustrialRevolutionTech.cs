namespace Model
{
    public class IndustrialRevolutionTech : TechTreeItemBase
    {
        public IndustrialRevolutionTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //increases suppliers production rate
        }
    }
}
