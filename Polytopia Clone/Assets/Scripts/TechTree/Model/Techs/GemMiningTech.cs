namespace Model
{
    public class GemMiningTech : TechTreeItemBase
    {
        public GemMiningTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect()
        {
            //Can build supplier on sandTile
        }
    }
}
