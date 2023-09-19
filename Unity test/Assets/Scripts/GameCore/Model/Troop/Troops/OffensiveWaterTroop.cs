using System.Collections.Generic;

namespace Model
{
    public abstract class OffensiveWaterTroop : WaterTroop
    {
        public override bool AttackedInTurn { get; protected set; }

        public OffensiveWaterTroop() : base()
        {
            GameManager.Get<TurnManagerBase>().OnTurnStarted +=
                (player) => AttackedInTurn = false;
        }

        public override List<TileBase> Attack(TroopBase troop)
        {
            if (!TilesInAttackRange.Contains(troop.Tile) || AttackedInTurn)
                return null;

            AttackedInTurn = true;
            bool damageTaken = troop.TakeDamage(TroopProperty.Damage);
            if (!damageTaken)
            {
                Player.RaiseOnAttackMissed(this, troop);
                return null;
            }

            return new List<TileBase>() { troop.Tile };
        }

        public override List<TileBase> Attack(BuildingBase building)
        {
            IList<TileBase> tilesInRange = TilesInAttackRange;
            tilesInRange.Add(Tile);
            if (!tilesInRange.Contains(building.Tile) || AttackedInTurn)
                return null;

            AttackedInTurn = true;
            building.TakeDamage(TroopProperty.Damage);
            return new List<TileBase>() { building.Tile };
        }

        public override void ApplyAllPropertyBonus(Player player)
        {
            base.ApplyAllPropertyBonus(player);
            TroopProperty.Damage += player.BonusProperty.OffensiveDamageBonus;
        }
    }
}