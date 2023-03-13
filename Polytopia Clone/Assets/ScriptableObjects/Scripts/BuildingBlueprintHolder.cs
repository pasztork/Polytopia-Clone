using UnityEngine;

[CreateAssetMenu]
public class BuildingBlueprintHolder : ScriptableObject
{
    [SerializeField] private BuildingBase[] blueprints;

    public BuildingBase[] Blueprints { get { return blueprints; } }
}