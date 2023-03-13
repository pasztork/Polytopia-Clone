using UnityEngine;

// TODO: Create base class for buildings
// Question: Do buildings differ in behaviour?
public class Bank : MonoBehaviour
{
    [Header("Production Settings")]
    [SerializeField] private int productionRate = 0;

    [Header("Cost Settings")]
    [SerializeField] private Cost cost;

    public Cost Cost { get { return cost; } }

    private void Awake()
    {
        ResourceContainer resourceContainer = BuildManager.Instance.ActiveResourceContainer;
        resourceContainer.Producers.Add(new MoneyProducer(resourceContainer, productionRate));
    }
}