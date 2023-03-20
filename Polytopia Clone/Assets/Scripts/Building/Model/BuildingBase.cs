using System.Collections.Generic;

namespace Model
{
    public abstract class BuildingBase
    {
        public Cost Cost { get; set; }

        protected IList<ProducerBase> producers;
        public IList<ProducerBase> Producers
        {
            get
            {
                producers ??= new List<ProducerBase>();
                return producers;
            }
            set => producers = value;
        }
    }
}
