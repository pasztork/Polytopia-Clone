using UnityEngine;

[CreateAssetMenu]
public class BuildingBlueprintHolder : ScriptableObject
{
    [SerializeField] private SerializableDictionary<string, BuildingBase> buildings;
    public SerializableDictionary<string, BuildingBase> Buildings { get { return buildings; } }
}