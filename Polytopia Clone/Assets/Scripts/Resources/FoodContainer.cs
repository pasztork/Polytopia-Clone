public class FoodProducer : ProducerBase
{
    public FoodProducer(ResourceContainer resourceContainer, int productionRate)
        : base(resourceContainer, productionRate) { }

    public override void Produce()
    {
        resourceContainer.FoodCount += productionRate;
    }
}