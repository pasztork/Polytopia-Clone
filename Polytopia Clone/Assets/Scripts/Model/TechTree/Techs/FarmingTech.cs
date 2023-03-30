namespace Model
{
    public class FarmingTech : TechTreeItemBase
    {
        public FarmingTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //Unlocks Farm building
        }
    }
}
