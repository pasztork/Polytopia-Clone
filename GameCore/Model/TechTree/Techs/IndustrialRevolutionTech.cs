namespace Model
{
    public class IndustrialRevolutionTech : TechTreeItemBase
    {
        private readonly int materialProductionBoost = 10;

        public IndustrialRevolutionTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["IndustrialRevolution"]));
            HashCode = "IndustrialRevolution";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.SupplierProductionBonus += materialProductionBoost;
            foreach (BuildingBase building in player.Buildings)
            {
                building.IncreaseMaterialProduction(materialProductionBoost);
            }
        }
    }
}
