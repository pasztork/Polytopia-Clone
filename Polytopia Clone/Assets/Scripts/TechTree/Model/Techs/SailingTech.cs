namespace Model
{
    public class SailingTech : TechTreeItemBase
    {
        public SailingTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //Unlocks Boat troop
        }
    }
}
