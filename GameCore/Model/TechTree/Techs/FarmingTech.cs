namespace Model
{
    public class FarmingTech : TechTreeItemBase
    {
        public FarmingTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Farming"]));
            HashCode = "Farming";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableBuildings.Add("Farm");
        }
    }
}
