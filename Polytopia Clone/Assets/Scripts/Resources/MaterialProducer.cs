public class MaterialProducer : ProducerBase
{
    public MaterialProducer(ResourceContainer resourceContainer, int productionRate)
        : base(resourceContainer, productionRate) { }

    public override void Produce()
    {
        resourceContainer.MaterialCount += productionRate;
    }
}