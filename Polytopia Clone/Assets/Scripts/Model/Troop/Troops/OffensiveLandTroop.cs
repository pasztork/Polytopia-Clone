using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Model
{
    public abstract class OffensiveLandTroop : LandTroop
    {
        protected bool attackedInTurn;

        public OffensiveLandTroop() : base()
        {
            GameManager.Get<TurnManagerBase>().OnTurnStarted +=
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

        public override void OffensiveLandMovementRangeBonus(Player player)
        {
            TroopProperty.MovementRange += player.BonusProperty.OffensiveLandMoveBonus;
        }

        public override void ApplyAllPropertyBonus(Player player)
        {
            base.ApplyAllPropertyBonus(player);
            TroopProperty.MovementRange += player.BonusProperty.OffensiveLandMoveBonus;
            TroopProperty.Damage += player.BonusProperty.OffensiveDamageBonus;
        }
    }
}