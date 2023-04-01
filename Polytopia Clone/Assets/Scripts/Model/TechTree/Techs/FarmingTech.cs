namespace Model
{
    public class FarmingTech : TechTreeItemBase
    {
        public FarmingTech(Cost cost) : base(cost)
        {
            HashCode = "Farming";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableBuildings.Add("Farm");
        }
    }
}
