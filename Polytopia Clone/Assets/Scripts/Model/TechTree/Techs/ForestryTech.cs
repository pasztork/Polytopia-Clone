namespace Model
{
    public class ForestryTech : TechTreeItemBase
    {
        public ForestryTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Forestry"]));
            HashCode = "Forestry";
        }
    }
}
