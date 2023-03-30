namespace Model
{
    public class TroopProperty
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int MovementRange { get; set; }
        public int AttackRange { get; set; }

        public TroopProperty(int health, int damage, int movementRange, int attackRange)
        {
            Health = health;
            Damage = damage;
            MovementRange = movementRange;
            AttackRange = attackRange;
        }
    }
}