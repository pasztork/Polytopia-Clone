namespace View
{
    public class Supplier : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase supplier = new Model.Supplier();
            supplier.OnDamageTaken += TakeDamage;
            supplier.BuildingProperty = new Model.BuildingProperty(BuildingProperties.Health, BuildingProperties.ProductionRate);
            supplier.Producers.Add(new Model.MaterialProducer(player.ResourceContainer, BuildingProperties.ProductionRate));
            supplier.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return supplier;
        }
    }
}