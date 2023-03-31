using System;
using System.Collections.Generic;

namespace Model
{
    public abstract class BuildingBase
    {
        // Owner player subscribes to event.
        public event Action<int> OnDamageTaken;

        // TODO: Remove this from BuildingBase and move to Requirements
        private Cost cost;
        public Cost Cost
        {
            get => cost;

            set
            {
                cost = value;
                Requirements.Cost = cost;
            }
        }

        public TileBase Tile { get; set; }
        public BuildingProperty BuildingProperty { get; set; }
        public Player Player { get; set; }
        public RequirementsListBase Requirements { get; set; }

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

        public virtual void DestroyEveryThingInRange(ISet<TileBase> availableTiles)
        {
            return;
        }

        public virtual bool CheckTechRequirement(GrassTile tile, Player player)
        {
            return true;
        }

        public virtual bool CheckTechRequirement(RockTile tile, Player player)
        {
            return false;
        }

        public virtual bool CheckTechRequirement(ForestTile tile, Player player)
        {
            return true;
        }

        public virtual bool CheckTechRequirement(SandTile tile, Player player)
        {
            return true;
        }

        public virtual bool CheckTechRequirement(WaterTile tile, Player player)
        {
            return false;
        }

        public virtual void IncreaseMoneyProduction(int amount)
        {
            return;
        }

        public virtual void IncreaseMaterialProduction(int amount)
        {
            return;
        }

        public virtual void IncreaseFoodProduction(int amount)
        {
            return;
        }

        public void ApplyAllPropertyBonus(Player player)
        {
            IncreaseMoneyProduction(player.BonusProperty.BankProductionBonus);
            IncreaseMaterialProduction(player.BonusProperty.SupplierProductionBonus);
            IncreaseFoodProduction(player.BonusProperty.FarmProductionBonus);
        }

        public bool TakeDamage(int damage)
        {
            BuildingProperty.Health -= damage;
            OnDamageTaken?.Invoke(BuildingProperty.Health);

            if (BuildingProperty.Health > 0)
                return false;

            StopProduction();
            Player.RemoveBuilding(this);
            Tile.BuildingOnTop = null;
            return true;
        }
    }
}
