namespace Model
{
    public class SailingTech : TechTreeItemBase
    {
        public SailingTech(Cost cost) : base(cost)
        {
            HashCode = "Sailing";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableTroops.Add("Boat");
        }
    }
}
