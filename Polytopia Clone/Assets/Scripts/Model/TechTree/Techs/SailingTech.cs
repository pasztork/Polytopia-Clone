namespace Model
{
    public class SailingTech : TechTreeItemBase
    {
        public SailingTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Sailing";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableTroops.Add("Boat");
        }
    }
}
