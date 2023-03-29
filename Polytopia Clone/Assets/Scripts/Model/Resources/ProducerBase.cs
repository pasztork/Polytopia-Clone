namespace Model
{
    public abstract class ProducerBase
    {
        protected ResourceContainer resourceContainer;
        protected int productionRate;

        public ProducerBase(ResourceContainer resourceContainer, int productionRate)
        {
            this.resourceContainer = resourceContainer;
            this.productionRate = productionRate;
            resourceContainer.Produce += Produce;
        }

        public void Unsubscribe()
        {
            resourceContainer.Produce -= Produce;
        }

        public abstract void Produce();
    }
}