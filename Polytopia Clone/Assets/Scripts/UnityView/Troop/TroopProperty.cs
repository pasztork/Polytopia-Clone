using UnityEngine;

namespace View
{
    [CreateAssetMenu]
    public class TroopProperty : ScriptableObject
    {
        [SerializeField] private int health;
        [SerializeField] private int damage;
        [SerializeField] private int movementRange;
        [SerializeField] private int attackRange;
        [SerializeField] private double dodgeRate;

        public int Health { get => health; }
        public int Damage { get => damage; }
        public int MovementRange { get => movementRange; }
        public int AttackRange { get => attackRange; }
        public double DodgeRate { get => dodgeRate; }
    }
}