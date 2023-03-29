namespace Model
{
    public class RidingTech : TechTreeItemBase
    {
        public RidingTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //+1 move range to troops
        }
    }
}
