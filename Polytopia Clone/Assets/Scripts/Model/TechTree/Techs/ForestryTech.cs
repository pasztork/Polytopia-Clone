namespace Model
{
    public class ForestryTech : TechTreeItemBase
    {
        public ForestryTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Forestry";
        }
    }
}
