using UnityEngine;

namespace Controller
{
    [CreateAssetMenu]
    public class Cost : ScriptableObject
    {
        [SerializeField] private int moneyCost;
        [SerializeField] private int materialCost;
        [SerializeField] private int foodCost;

        public int MoneyCost { get => moneyCost; }
        public int MaterialCost { get => materialCost; }
        public int FoodCost { get => foodCost; }
    }
}