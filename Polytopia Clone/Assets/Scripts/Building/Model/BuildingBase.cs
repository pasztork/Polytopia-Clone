using System.Collections.Generic;

namespace Model
{
    public abstract class BuildingBase
    {
        public Cost Cost { get; set; }
        public TileBase Tile { get; set; }

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

        public void StopProduction()
        {
            foreach (var producer in producers)
                producer.Unsubscribe();
        }

        public virtual bool TrainTroop(TroopBase troop)
        {
            return false;
        }

        public virtual IList<TileBase> GetTilesInRange()
        {
            return null;
        }
    }
}
