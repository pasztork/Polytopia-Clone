namespace Model
{
    public class ForestryTech : TechTreeItemBase
    {
        public ForestryTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //Can build Supplier on forest
        }
    }
}
