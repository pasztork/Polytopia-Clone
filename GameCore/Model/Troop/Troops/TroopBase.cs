namespace Model
{
    public abstract class TroopBase
    {
        public static Dictionary<string, JsonTroop> TroopProperties { get; set; } = null;

        // The argument of the method is the remainging health.
        public event Action<int> OnDamageTaken;
        public event Action<int> OnTroopHealed;

        public Cost Cost { get; set; }
        public int MaxHealth { private get; set; }
        public TroopProperty TroopProperty { get; set; }
        public TileBase Tile { get; set; }
        public Player Player { get; set; }

        protected bool movedInTurn = false;

        // Doesn't contain Tile.
        public IList<TileBase> TilesInMovementRange { get => GetTilesInRange(TroopProperty.MovementRange); }
        public IList<TileBase> TilesInAttackRange { get => GetTilesInRange(TroopProperty.AttackRange); }

        protected JsonTroop initialValues = null;

        public TroopBase()
        {
            GameManager.Get<TurnManagerBase>().OnTurnStarted +=
                (player) => movedInTurn = false;
        }

        public bool Move(TileBase target)
        {
            if (!TilesInMovementRange.Contains(target) || movedInTurn)
                return false;

            bool accepted = target.AcceptTroop(this);
            return accepted;
        }

        // These are used to remove typechecking.
        public abstract bool Relocate(TraversableTile target);

        public abstract bool Relocate(WaterTile target);

        public virtual bool Relocate(RockTile target)
        {
            return false;
        }

        public abstract bool Train(TraversableTile tile);

        public abstract bool Train(NonTraversableTile tile);

        public abstract bool Train(WaterTile tile);

        public virtual List<TileBase> Attack(TroopBase troop)
        {
            return null;
        }

        public virtual List<TileBase> Attack(BuildingBase building)
        {
            return null;
        }

        public virtual void WaterMovementRangeBonus(Player player)
        {
            return;
        }

        public virtual void OffensiveLandMovementRangeBonus(Player player)
        {
            return;
        }

        public virtual void ApplyAllPropertyBonus(Player player)
        {
            TroopProperty.DodgeRate += player.BonusProperty.DodgeBonus;
        }

        public void Heal(Player player)
        {
            if (player != Player || !Player.AvailableTiles.Contains(Tile))
                return;

            TroopProperty.Health += Player.BonusProperty.HealAmount;
            if (TroopProperty.Health > MaxHealth)
                TroopProperty.Health = MaxHealth;

            OnTroopHealed?.Invoke(TroopProperty.Health);
        }

        public bool TakeDamage(int damage)
        {
            bool dodged = new Random().NextDouble() <= TroopProperty.DodgeRate;
            if (dodged && !GameManager.IsGameplayDeterministic)
                return false;

            TroopProperty.Health -= damage;
            OnDamageTaken?.Invoke(TroopProperty.Health);

            if (TroopProperty.Health > 0)
                return true;

            GameManager.Get<TurnManagerBase>().OnTurnStarted -= Heal;
            Player.Troops.Remove(this);
            Tile.TroopOnTop = null;
            return true;
        }

        public bool Kill()
        {
            TroopProperty.Health = 0;
            GameManager.Get<TurnManagerBase>().OnTurnStarted -= Heal;
            OnDamageTaken?.Invoke(TroopProperty.Health);

            Player.Troops.Remove(this);
            Tile.TroopOnTop = null;
            return true;
        }

        protected virtual IList<TileBase> GetTilesInRange(int range)
        {
            ISet<TileBase> reachables = new HashSet<TileBase> { Tile };
            for (int i = 0; i < range; i++)
            {
                ISet<TileBase> toAdd = new HashSet<TileBase>();
                foreach (TileBase reachable in reachables)
                    foreach (TileBase tile in reachable.Neighbors)
                        toAdd.Add(tile);

                foreach (TileBase tile in toAdd)
                    reachables.Add(tile);
            }
            reachables.Remove(Tile);
            return reachables.ToList();
        }

        public virtual void FillRequirements(RequirementsListBase requirements)
        {
            // Do nothing.
        }

        protected void Init(Player player)
        {
            if (initialValues == null)
            {
                throw new ArgumentException("You must set initialValues field before initialising!");
            }

            Player = player;
            TroopProperty = new TroopProperty
            {
                Health = initialValues.Health,
                Damage = initialValues.Damage,
                MovementRange = initialValues.MovementRange,
                AttackRange = initialValues.AttackRange,
                DodgeRate = initialValues.DodgeRate
            };
            MaxHealth = TroopProperty.Health;
            Cost = new Cost(initialValues.Cost.Money, initialValues.Cost.Material, initialValues.Cost.Food);
        }
    }
}
