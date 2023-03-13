using UnityEngine;

[CreateAssetMenu]
public class Cost : ScriptableObject
{
    [SerializeField] private int moneyCost = 0;
    [SerializeField] private int materialCost = 0;
    [SerializeField] private int foodCost = 0;

    public int MoneyCost { get { return moneyCost; } }
    public int MaterialCost { get { return materialCost; } }
    public int FoodCost { get { return foodCost; } }
}
