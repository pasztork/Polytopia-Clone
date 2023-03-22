namespace Model
{
    public class Scout : TroopBase
    {
        public override bool Attack(TroopBase troop)
        {
            if (!TilesInAttackRange.Contains(troop.Tile))
                return false;

            troop.TakeDamage(TroopProperty.Damage);
            return true;
        }
    }
}