namespace View
{
    public class Bank : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase bank = new Model.Bank();
            bank.OnDamageTaken += TakeDamage;
            bank.BuildingProperty = new Model.BuildingProperty(BuildingProperties.Health, BuildingProperties.ProductionRate);
            bank.Producers.Add(new Model.MoneyProducer(player.ResourceContainer, BuildingProperties.ProductionRate));
            bank.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return bank;
        }
    }
}