using UnityEngine;

namespace View
{
    [CreateAssetMenu]
    public class BuildingProperty : ScriptableObject
    {
        [SerializeField] private int health;
        public int Health { get => health; }
    }
}

