using UnityEngine;

public class Bank : BuildingBase
{
    [Header("Production Settings")]
    [SerializeField] private int productionRate;

    private void Awake()
    {
        ResourceContainer resourceContainer = BuildManager.Instance.ActiveResourceContainer;
        SetupProducer(new MoneyProducer(resourceContainer, productionRate));
    }
}