namespace Model
{
    public class HarborTech : TechTreeItemBase
    {
        public HarborTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect()
        {
            //Unlocks Harbor building
        }
    }
}
