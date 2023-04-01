namespace Model
{
    public class HarborTech : TechTreeItemBase
    {
        public HarborTech(Cost cost) : base(cost)
        {
            HashCode = "Harbor";
        }

    public override void ActivateEffect(Player player)
        {
            player.AvailableBuildings.Add("Harbor");
        }
    }
}
