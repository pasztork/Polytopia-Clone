using UnityEngine;

public class BuildingHolder : MonoBehaviour
{
    public BuildingBase BuildingOnTop { get; set; } = null;

    public bool IsEmpty { get => BuildingOnTop == null; }

    private void OnMouseDown()
    {
        BuildManager.Instance.ActiveBuildingHolder = this;
    }
}