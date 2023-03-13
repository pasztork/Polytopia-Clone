using UnityEngine;

public abstract class BuildingBase : MonoBehaviour
{
    [Header("Cost Settings")]
    [SerializeField] private Cost cost;

    public Cost Cost { get { return cost; } }
    public bool IsAvailable { get; set; } = true;

    // Can be used to create buildings that produce more than one type of resource
    protected void SetupProducer(ProducerBase producer)
    {
        ResourceContainer resourceContainer = BuildManager.Instance.ActiveResourceContainer;
        resourceContainer.Producers.Add(producer);
    }
}
