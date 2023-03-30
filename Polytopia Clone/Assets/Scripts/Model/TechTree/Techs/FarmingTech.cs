namespace Model
{
    public class FarmingTech : TechTreeItemBase
    {
        public FarmingTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Farming";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableBuildings.Add("Farm");
        }
    }
}
