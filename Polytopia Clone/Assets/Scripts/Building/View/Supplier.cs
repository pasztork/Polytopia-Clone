namespace View
{
    public class Supplier : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase supplier = new Model.Supplier();
            supplier.OnDamageTaken += TakeDamage;
            supplier.BuildingProperty = new Model.BuildingProperty(buildingProperties.Health);
            supplier.Producers.Add(new Model.MaterialProducer(player.ResourceContainer, productionRate));
            supplier.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return supplier;
        }
    }
}