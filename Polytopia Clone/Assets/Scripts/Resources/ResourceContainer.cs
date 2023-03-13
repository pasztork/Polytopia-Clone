using System.Collections.Generic;
using UnityEngine;


public class ResourceContainer : MonoBehaviour
{
    // This should only be used by those objects that produce resources
    public delegate void ProduceDelegate();
    public event ProduceDelegate Produce;

    public IList<ProducerBase> Producers { get; private set; }

    [Header("Starting Resources")]
    [SerializeField] private int moneyCount = 0;
    [SerializeField] private int materialCount = 0;
    [SerializeField] private int foodCount = 0;

    public int MoneyCount { get => moneyCount; set { InfoPanel.Instance.UpdateContent(); moneyCount = value; } }
    public int MaterialCount { get => materialCount; set { InfoPanel.Instance.UpdateContent(); materialCount = value; } }
    public int FoodCount { get => foodCount; set { InfoPanel.Instance.UpdateContent(); foodCount = value; } }

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