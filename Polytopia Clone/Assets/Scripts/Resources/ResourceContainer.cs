using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceContainer : MonoBehaviour
{
    public event Action Produce;

    public IList<ProducerBase> Producers { get; private set; }

    [Header("Starting Resources")]
    [SerializeField] private int moneyCount = 0;
    [SerializeField] private int materialCount = 0;
    [SerializeField] private int foodCount = 0;

    public int MoneyCount { get => moneyCount; set { moneyCount = value;  InfoPanel.Instance.UpdateContent(); } }
    public int MaterialCount { get => materialCount; set { materialCount = value;  InfoPanel.Instance.UpdateContent(); } }
    public int FoodCount { get => foodCount; set { foodCount = value;  InfoPanel.Instance.UpdateContent(); } }

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
        BuildManager.Instance.ActiveResourceContainer = this;
        TrainManager.Instance.ActiveResourceContainer = this;
        Produce?.Invoke();
    }

    public bool HasEnoughFor(Cost cost)
    {
        return
            MoneyCount >= cost.MoneyCost &&
            MaterialCount >= cost.MaterialCost &&
            FoodCount >= cost.FoodCost;
    }

    public override string ToString()
    {
        return $"Money: {MoneyCount}\nMaterial: {MaterialCount}\nFood: {FoodCount}";
    }

    public static ResourceContainer operator -(ResourceContainer resourceContainer, Cost cost)
    {
        resourceContainer.MoneyCount -= cost.MoneyCost;
        resourceContainer.MaterialCount -= cost.MaterialCost;
        resourceContainer.FoodCount -= cost.FoodCost;
        return resourceContainer;
    }
}