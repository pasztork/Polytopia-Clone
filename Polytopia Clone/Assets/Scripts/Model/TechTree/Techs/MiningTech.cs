namespace Model
{
    public class MiningTech : TechTreeItemBase
    {
        public MiningTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Mining";
        }

        public override void ActivateEffect(Player player)
        {
            //Can build supplier on rockTile
        }
    }
}
