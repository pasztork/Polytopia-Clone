namespace View
{
    public class Harbor : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase harbor = new Model.Harbor();
            harbor.OnDamageTaken += TakeDamage;
            harbor.BuildingProperty = new Model.BuildingProperty(BuildingProperties.Health, BuildingProperties.ProductionRate);
            harbor.Producers.Add(new Model.MoneyProducer(player.ResourceContainer, BuildingProperties.ProductionRate));
            harbor.Producers.Add(new Model.FoodProducer(player.ResourceContainer, BuildingProperties.ProductionRate));
            harbor.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return harbor;
        }
    }
}
