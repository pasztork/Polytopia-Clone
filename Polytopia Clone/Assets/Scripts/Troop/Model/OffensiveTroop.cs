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
    }
}