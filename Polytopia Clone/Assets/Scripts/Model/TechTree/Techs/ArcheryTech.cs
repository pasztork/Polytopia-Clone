namespace Model
{
    public class ArcheryTech : TechTreeItemBase
    {
        public ArcheryTech(Cost cost) : base(cost)
        {
            HashCode = "Archery";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableTroops.Add("Archer");
        }
    }
}
