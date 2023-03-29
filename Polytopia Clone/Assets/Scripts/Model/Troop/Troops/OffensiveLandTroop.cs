using System.Collections.Generic;

namespace Model
{
    public abstract class OffensiveLandTroop : LandTroop
    {
        protected bool attackedInTurn;

        public OffensiveLandTroop() : base()
        {
            DependencyContainer.Get<TurnManagerBase>().OnTurnStarted +=
                (player) => attackedInTurn = false;
        }

        public override bool Attack(TroopBase troop)
        {
            if (!TilesInAttackRange.Contains(troop.Tile) || attackedInTurn)
                return false;

            attackedInTurn = true;
            troop.TakeDamage(TroopProperty.Damage);
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
            return true;
        }
    }
}