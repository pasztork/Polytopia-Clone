using UnityEngine;

[CreateAssetMenu]
public class BuildingBlueprintHolder : ScriptableObject
{
    [SerializeField] private SerializableDictionary<string, BuildingBase> buildings;

    public BuildingBase this[string name]
    {
        get => buildings[name];
    }
}