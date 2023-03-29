namespace Model
{
    public class MathematicsTech : TechTreeItemBase
    {
        public MathematicsTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect()
        {
            //lowers building cost
        }
    }
}
