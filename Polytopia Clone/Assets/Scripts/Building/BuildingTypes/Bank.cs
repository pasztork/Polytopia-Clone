using UnityEngine;

public class Bank : BuildingBase
{
    [Header("Production Settings")]
    [SerializeField] private int productionRate = 0;

    private void Awake()
    {
        ResourceContainer resourceContainer = BuildManager.Instance.ActiveResourceContainer;
        SetupProducer(new MoneyProducer(resourceContainer, productionRate));
    }
}