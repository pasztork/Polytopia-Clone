using System;
using System.Collections.Generic;

namespace Model
{
    public abstract class BuildingBase
    {
        public static Dictionary<string, JsonBuilding> BuildingProperties { get; set; } = null;

        // Owner player subscribes to event.
        public event Action<int> OnDamageTaken;

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

        public virtual bool TroopTrained
        {
            get => false;
            protected set { }
        }

        public TileBase Tile { get; set; }
        public BuildingProperty BuildingProperty { get; set; }
        public List<string> TrainableTroops { get; protected set; } = new List<string>();
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

        protected JsonBuilding initialValues = null;

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

        protected void Init(Player player)
        {
            if (initialValues == null)
            {
                throw new ArgumentException("You must set initialValues field before initialising!");
            }

            Player = player;
            BuildingProperty = new BuildingProperty
            {
                Health = initialValues.Health,
                Range = initialValues.Range,
                FoodProductionRate = initialValues.ProductionRate.Food,
                MaterialProductionRate = initialValues.ProductionRate.Material,
                MoneyProductionRate = initialValues.ProductionRate.Money
            };
            Producers.Add(new FoodProducer(Player.ResourceContainer, initialValues.ProductionRate.Food));
            Producers.Add(new MaterialProducer(Player.ResourceContainer, initialValues.ProductionRate.Material));
            Producers.Add(new MoneyProducer(Player.ResourceContainer, initialValues.ProductionRate.Money));
            Cost = new Cost(initialValues.Cost.Money, initialValues.Cost.Material, initialValues.Cost.Food);
        }
    }
}
