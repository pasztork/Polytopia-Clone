using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public abstract class TroopBase
    {
        public event Action OnDied;

        public Cost Cost { get; set; }
        public TroopProperty TroopProperty { get; set; }
        public TileBase Tile { get; set; }
        public Player Player { get; set; }

        // Doesn't contain Tile.
        public IList<TileBase> TilesInMovementRange { get => GetTilesInRange(TroopProperty.MovementRange); }
        public IList<TileBase> TilesInAttackRange { get => GetTilesInRange(TroopProperty.AttackRange); }

        public bool Move(TileBase target)
        {
            if (!TilesInMovementRange.Contains(target))
                return false;

            Tile = target;
            return true;
        }

        public bool Attack(TroopBase troop)
        {
            if (!TilesInAttackRange.Contains(troop.Tile))
                return false;

            troop.TakeDamage(TroopProperty.Damage);
            return true;
        }

        // Tells wheter or not troop died.
        public bool TakeDamage(int damage)
        {
            TroopProperty.Health -= damage;

            if (TroopProperty.Health > 0)
                return false;

            Player.Troops.Remove(this);
            Tile.TroopOnTop = null;
            OnDied?.Invoke();
            return true;
        }

        private IList<TileBase> GetTilesInRange(int range)
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
    }
}
