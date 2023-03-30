namespace Model
{
    public class CatapultTech : TechTreeItemBase
    {
        public CatapultTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Catapult";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableTroops.Add("Cataput");
        }
    }
}
