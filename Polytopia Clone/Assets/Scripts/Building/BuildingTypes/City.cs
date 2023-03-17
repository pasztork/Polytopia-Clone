using UnityEngine;

public class City : BuildingBase
{
    [Header("Production Settings")]
    [SerializeField] private int productionRate = 0;

    private void Awake()
    {
        ResourceContainer resourceContainer = BuildManager.Instance.ActiveResourceContainer;
        SetupProducer(new MoneyProducer(resourceContainer, productionRate));
        SetupProducer(new MaterialProducer(resourceContainer, productionRate));
        SetupProducer(new FoodProducer(resourceContainer, productionRate));
    }
}