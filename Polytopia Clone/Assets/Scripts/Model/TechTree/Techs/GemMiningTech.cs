namespace Model
{
    public class GemMiningTech : TechTreeItemBase
    {
        public GemMiningTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "GemMining";
        }
    }
}
