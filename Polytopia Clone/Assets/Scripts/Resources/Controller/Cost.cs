using UnityEngine;

namespace Controller
{
    [CreateAssetMenu]
    public class Cost : ScriptableObject
    {
        [SerializeField] private int moneyCost;
        [SerializeField] private int materialCost;
        [SerializeField] private int foodCost;

        public Cost(int moneyCost, int materialCost, int foodCost)
        {
            this.moneyCost = moneyCost;
            this.materialCost = materialCost;
            this.foodCost = foodCost;
        }

        public int MoneyCost { get => moneyCost; }
        public int MaterialCost { get => materialCost; }
        public int FoodCost { get => foodCost; }
    }
}