public class MoneyProducer : ProducerBase
{
    public MoneyProducer(ResourceContainer resourceContainer, int productionRate)
        : base(resourceContainer, productionRate) { }

    public override void Produce()
    {
        resourceContainer.MoneyCount += productionRate;
    }
}