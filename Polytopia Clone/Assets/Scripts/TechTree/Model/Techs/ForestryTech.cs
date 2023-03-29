namespace Model
{
    public class ForestryTech : TechTreeItemBase
    {
        public ForestryTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect()
        {
            //Can build Supplier on forest
        }
    }
}
