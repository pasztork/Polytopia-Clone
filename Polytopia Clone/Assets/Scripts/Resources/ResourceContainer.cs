using System.Collections.Generic;
using UnityEngine;

public delegate void ProduceDelegate();

public class ResourceContainer : MonoBehaviour
{
    public event ProduceDelegate Produce;

    public IList<ProducerBase> Producers { get; private set; }

    [Header("Starting Resources")]
    [SerializeField] private int moneyCount = 0;
    [SerializeField] private int materialCount = 0;
    [SerializeField] private int foodCount = 0;

    public int MoneyCount { get => moneyCount; set => moneyCount = value; }
    public int MaterialCount { get => materialCount; set => materialCount = value; }
    public int FoodCount { get => foodCount; set => foodCount = value; }

    [Header("Base Production")]
    [SerializeField] private int baseMoneyProductionRate = 5;
    [SerializeField] private int baseMaterialProductionRate = 5;
    [SerializeField] private int baseFoodProductionRate = 5;

    private void Awake()
    {
        Producers = new List<ProducerBase>()
        {
            new MoneyProducer(this, baseMoneyProductionRate),
            new MaterialProducer(this, baseMaterialProductionRate),
            new FoodProducer(this, baseFoodProductionRate)
        };
    }

    public void StartTurn()
    {
        Produce?.Invoke();
    }

    public override string ToString()
    {
        return $"Money: {MoneyCount}\nMaterial: {MaterialCount}\nFood: {FoodCount}";
    }
}