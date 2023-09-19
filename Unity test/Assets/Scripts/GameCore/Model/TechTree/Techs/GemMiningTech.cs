namespace Model
{
    public class GemMiningTech : TechTreeItemBase
    {
        public GemMiningTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["GemMining"]));
            HashCode = "GemMining";
        }
    }
}
