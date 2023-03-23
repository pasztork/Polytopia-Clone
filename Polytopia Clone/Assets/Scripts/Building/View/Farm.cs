namespace View
{
    public class Farm : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase farm = new Model.Farm();
            farm.Producers.Add(new Model.FoodProducer(player.ResourceContainer, productionRate));
            farm.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return farm;
        }
    }
}