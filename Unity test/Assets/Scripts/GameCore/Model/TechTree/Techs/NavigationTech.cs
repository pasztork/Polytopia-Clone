namespace Model
{
    public class NavigationTech : TechTreeItemBase
    {
        private readonly int offensiveWaterMoveBonus = 1;

        public NavigationTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Navigation"]));
            HashCode = "Navigation";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.WaterMoveBonus += offensiveWaterMoveBonus;
            foreach (TroopBase troop in player.Troops)
            {
                troop.WaterMovementRangeBonus(player);
            }
        }
    }
}
