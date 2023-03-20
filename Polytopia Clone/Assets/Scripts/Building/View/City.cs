namespace View
{
    public class City : BuildingBase
    {
        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            FireOnBuildingClickedEvent();
        }

        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase city = new Model.City();
            city.Producers.Add(new Model.MoneyProducer(player.ResourceContainer, productionRate));
            city.Producers.Add(new Model.MaterialProducer(player.ResourceContainer, productionRate));
            city.Producers.Add(new Model.FoodProducer(player.ResourceContainer, productionRate));
            city.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return city;
        }
    }
}