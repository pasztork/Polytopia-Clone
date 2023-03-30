namespace Model
{
    public class ForestryTech : TechTreeItemBase
    {
        public ForestryTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Forestry";
        }

        public override void ActivateEffect(Player player)
        {
            //Can build Supplier on forest
        }
    }
}
