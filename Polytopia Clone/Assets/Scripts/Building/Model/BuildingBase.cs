using System.Collections.Generic;

namespace Model
{
    public abstract class BuildingBase
    {
        public Cost Cost { get; }

        protected IList<ProducerBase> producers;
        public IList<ProducerBase> Producers
        {
            get => producers;
            set => producers = value;
        }
    }
}
