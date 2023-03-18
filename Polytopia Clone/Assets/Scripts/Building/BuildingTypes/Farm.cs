using UnityEngine;

public class Farm : BuildingBase
{
    [Header("Production Settings")]
    [SerializeField] private int productionRate;

    private void Awake()
    {
        ResourceContainer resourceContainer = BuildManager.Instance.ActiveResourceContainer;
        SetupProducer(new FoodProducer(resourceContainer, productionRate));
    }
}