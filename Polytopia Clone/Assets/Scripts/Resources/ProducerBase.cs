public abstract class ProducerBase
{
    protected ResourceContainer resourceContainer;
    protected int productionRate = 0;

    public ProducerBase(ResourceContainer resourceContainer, int productionRate)
    {
        this.resourceContainer = resourceContainer;
        this.productionRate = productionRate;
        resourceContainer.Produce += Produce;
    }

    public abstract void Produce();
}