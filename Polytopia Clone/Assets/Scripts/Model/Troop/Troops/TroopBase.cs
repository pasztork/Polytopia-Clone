using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public abstract class TroopBase
    {
        // The argument of the method is the remainging health.
        public event Action<int> OnDamageTaken;

        public Cost Cost { get; set; }
        public TroopProperty TroopProperty { get; set; }
        public TileBase Tile { get; set; }
        public Player Player { get; set; }

        protected bool movedInTurn = false;

        // Doesn't contain Tile.
        public IList<TileBase> TilesInMovementRange { get => GetTilesInRange(TroopProperty.MovementRange); }
        public IList<TileBase> TilesInAttackRange { get => GetTilesInRange(TroopProperty.AttackRange); }

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

        public virtual bool Attack(TroopBase troop)
        {
            return false;
        }

        public virtual bool Attack(BuildingBase building)
        {
            return false;
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
            TroopProperty.DodgeRate += player.TroopBonus.DodgeBonus;
        }

        // Tells whether or not troop died.
        public bool TakeDamage(int damage)
        {
            bool dodged = new Random().NextDouble() <= TroopProperty.DodgeRate;
            if (dodged)
                return false;

            TroopProperty.Health -= damage;
            OnDamageTaken?.Invoke(TroopProperty.Health);

            if (TroopProperty.Health > 0)
                return false;

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
    }
}
