namespace Model
{
    public class IndustrialRevolutionTech : TechTreeItemBase
    {
        private readonly int materialProductionBoost = 10;
        public IndustrialRevolutionTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "IndustrialRevolution";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.SupplierProductionBonus += materialProductionBoost;
            foreach(BuildingBase building in player.Buildings)
            {
                building.IncreaseMaterialProduction(materialProductionBoost);
            }
        }
    }
}
