namespace View
{
    public class Farm : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase farm = new Model.Farm();
            farm.Producers.Add(new Model.FoodProducer(player.ResourceContainer, productionRate));
            return farm;
        }
    }
}