using UnityEngine;

public abstract class TroopBase : MonoBehaviour
{
    [Header("Cost Settings")]
    [SerializeField] private Cost cost;
    [SerializeField] private TroopProperty troopProperty;
    public Cost Cost { get => cost; }
    public TroopProperty TroopProperty { get => troopProperty; }
}
