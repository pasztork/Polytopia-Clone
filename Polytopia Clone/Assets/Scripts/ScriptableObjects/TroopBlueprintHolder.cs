using UnityEngine;

[CreateAssetMenu]
public class TroopBlueprintHolder : ScriptableObject
{
    [SerializeField] private SerializableDictionary<string, TroopBase> troops;

    public TroopBase this[string name]
    {
        get => troops[name];
    }
}
