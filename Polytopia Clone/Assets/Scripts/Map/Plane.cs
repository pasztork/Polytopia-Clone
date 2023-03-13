using UnityEngine;
using UnityEngine.EventSystems;

public class Plane : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        VisibilityManager.HideAll();
    }
}
