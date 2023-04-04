namespace Model
{
    public class MiningTech : TechTreeItemBase
    {
        public MiningTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Mining"]));
            HashCode = "Mining";
        }
    }
}
