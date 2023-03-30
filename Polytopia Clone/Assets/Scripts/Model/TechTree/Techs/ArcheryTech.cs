namespace Model
{
    public class ArcheryTech : TechTreeItemBase
    {
        public ArcheryTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Archery";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableTroops.Add("Archer");
        }
    }
}
