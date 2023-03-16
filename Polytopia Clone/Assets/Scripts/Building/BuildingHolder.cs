using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingHolder : MonoBehaviour
{
    public BuildingBase BuildingOnTop { get; set; } = null;

    public bool IsEmpty { get => BuildingOnTop == null; }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        BuildManager.Instance.ActiveBuildingHolder = this;
    }
}