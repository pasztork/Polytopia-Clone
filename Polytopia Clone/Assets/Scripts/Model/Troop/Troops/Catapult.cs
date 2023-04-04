using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Catapult : OffensiveLandTroop
    {
        public Catapult(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties.Catapult;
            Init(player);
        }

        public override bool Attack(TroopBase troop)
        {
            if (!TilesInAttackRange.Contains(troop.Tile) || attackedInTurn)
                return false;

            attackedInTurn = true;
            troop.TakeDamage(TroopProperty.Damage);

            if (troop.Tile.BuildingOnTop != null && troop.Tile.BuildingOnTop.Player != Player)
                troop.Tile.BuildingOnTop.TakeDamage(TroopProperty.Damage);

            AttackNeighbors(troop.Tile);
            return true;
        }

        public override bool Attack(BuildingBase building)
        {
            IList<TileBase> tilesInRange = TilesInAttackRange;
            tilesInRange.Add(Tile);

            if (!tilesInRange.Contains(building.Tile) || attackedInTurn)
                return false;

            attackedInTurn = true;
            building.TakeDamage(TroopProperty.Damage);

            if (building.Tile.TroopOnTop != null && building.Tile.TroopOnTop.Player != Player)
                building.Tile.TroopOnTop.TakeDamage(TroopProperty.Damage);

            AttackNeighbors(building.Tile);
            return true;
        }

        private void AttackNeighbors(TileBase tile)
        {
            foreach (TileBase neighbor in tile.Neighbors)
            {
                if (neighbor.TroopOnTop != null && neighbor.TroopOnTop.Player != Player)
                {
                    neighbor.TroopOnTop.TakeDamage(TroopProperty.Damage / 2);
                }

                if (neighbor.BuildingOnTop != null && neighbor.BuildingOnTop.Player != Player)
                {
                    neighbor.BuildingOnTop.TakeDamage(TroopProperty.Damage / 2);
                }
            }
        }

        protected override IList<TileBase> GetTilesInRange(int range)
        {
            IList<TileBase> allTiles = base.GetTilesInRange(range);
            IList<TileBase> notReachables = base.GetTilesInRange(range - 1);

            var reachables = allTiles.ToHashSet();
            foreach (TileBase tile in notReachables)
            {
                reachables.Remove(tile);
            }

            reachables.Remove(Tile);
            return reachables.ToList();
        }
        public override string ToString()
        {
            return "Catapult";
        }
    }
}