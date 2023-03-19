using UnityEngine;

public class City : BuildingBase
{
    [Header("Production Settings")]
    [SerializeField] private int productionRate;

    private void Awake()
    {
        ResourceContainer resourceContainer = BuildManager.Instance.ActiveResourceContainer;
        SetupProducer(new MoneyProducer(resourceContainer, productionRate));
        SetupProducer(new MaterialProducer(resourceContainer, productionRate));
        SetupProducer(new FoodProducer(resourceContainer, productionRate));
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        FireOnBuildingClickedEvent();
    }
}