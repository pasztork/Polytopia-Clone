namespace View
{
    public class Supplier : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase supplier = new Model.Supplier();
            supplier.Producers.Add(new Model.MaterialProducer(player.ResourceContainer, productionRate));
            return supplier;
        }
    }
}