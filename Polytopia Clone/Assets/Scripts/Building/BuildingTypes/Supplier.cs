using UnityEngine;

public class Supplier : BuildingBase
{
    [Header("Production Settings")]
    [SerializeField] private int productionRate = 0;

    private void Awake()
    {
        ResourceContainer resourceContainer = BuildManager.Instance.ActiveResourceContainer;
        SetupProducer(new MaterialProducer(resourceContainer, productionRate));
    }
}