namespace Model
{
    public class HarborTech : TechTreeItemBase
    {
        public HarborTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Harbor"]));
            HashCode = "Harbor";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableBuildings.Add("Harbor");
        }
    }
}
