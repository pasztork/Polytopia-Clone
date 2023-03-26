namespace Model
{
    public abstract class OffensiveTroop : TroopBase
    {
        private bool attackedInTurn;

        public OffensiveTroop() : base()
        {
            TurnManager.Instance.OnTurnStarted +=
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
            var tilesInRange = TilesInAttackRange;
            tilesInRange.Add(Tile);
            if (!tilesInRange.Contains(building.Tile) || attackedInTurn)
                return false;

            attackedInTurn = true;
            building.TakeDamage(TroopProperty.Damage);
            return true;
        }
    }
}