namespace Model
{
    public class CatapultTech : TechTreeItemBase
    {
        public CatapultTech(Cost cost) : base(cost)
        {
            HashCode = "Catapult";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableTroops.Add("Catapult");
        }
    }
}
