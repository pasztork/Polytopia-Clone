namespace View
{
    public class Harbor : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase harbor = new Model.Harbor();
            harbor.OnDamageTaken += TakeDamage;
            harbor.BuildingProperty = new Model.BuildingProperty(buildingProperties.Health);
            harbor.Producers.Add(new Model.MoneyProducer(player.ResourceContainer, productionRate));
            harbor.Producers.Add(new Model.FoodProducer(player.ResourceContainer, productionRate));
            harbor.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return harbor;
        }
    }
}
