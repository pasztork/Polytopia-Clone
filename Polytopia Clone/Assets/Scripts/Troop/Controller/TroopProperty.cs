using UnityEngine;

namespace Controller
{
    [CreateAssetMenu]
    public class TroopProperty : ScriptableObject
    {
        [SerializeField] private int health;
        [SerializeField] private int damage;
        [SerializeField] private int movementRange;
        [SerializeField] private int attackRange;

        public int Health { get => health; }
        public int Damage { get => damage; }
        public int MovementRange { get => movementRange; }
        public int AttackRange { get => attackRange; }
    }
}