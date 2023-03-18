using UnityEngine;

public class Supplier : BuildingBase
{
    [Header("Production Settings")]
    [SerializeField] private int productionRate;

    private void Awake()
    {
        ResourceContainer resourceContainer = BuildManager.Instance.ActiveResourceContainer;
        SetupProducer(new MaterialProducer(resourceContainer, productionRate));
    }
}