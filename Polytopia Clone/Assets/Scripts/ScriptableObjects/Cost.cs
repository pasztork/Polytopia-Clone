using UnityEngine;

[CreateAssetMenu]
public class Cost : ScriptableObject
{
    [SerializeField] private int moneyCost;
    [SerializeField] private int materialCost;
    [SerializeField] private int foodCost;

    public int MoneyCost { get { return moneyCost; } }
    public int MaterialCost { get { return materialCost; } }
    public int FoodCost { get { return foodCost; } }
}
