using System;
using System.Collections.Generic;

namespace Model
{
    public abstract class BuildingBase
    {
        // Owner player subscribes to event.
        public event Action<BuildingBase> OnBuildingDestroyed;

        public Cost Cost { get; set; }
        public TileBase Tile { get; set; }
        public int Health { private get; set; }

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
            return new List<TileBase>();
        }

        public bool TakeDamage(int damage)
        {
            Health -= damage;
            if (Health > 0)
                return false;

            Tile.BuildingOnTop = null;
            OnBuildingDestroyed.Invoke(this);
            return true;
        }
    }
}
