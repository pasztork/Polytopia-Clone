using UnityEngine;

namespace View
{
    [CreateAssetMenu]
    public class BuildingProperty : ScriptableObject
    {
        [SerializeField] private int health;
        [SerializeField] private int productionRate;
        public int Health { get => health; }
        public int ProductionRate { get => productionRate; }
    }
}

