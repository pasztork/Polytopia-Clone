namespace Model
{
    public class IrrigationTech : TechTreeItemBase
    {
        private readonly int foodProductionBoost = 10;
        public IrrigationTech(Cost cost) : base(cost)
        {
            HashCode = "Irrigation";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.FarmProductionBonus += foodProductionBoost;
            foreach (BuildingBase building in player.Buildings)
            {
                building.IncreaseFoodProduction(foodProductionBoost);
            }
        }
    }
}
