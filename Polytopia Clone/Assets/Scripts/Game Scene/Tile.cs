using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    // Parameters
    [SerializeField] Color highlightColor;
    [SerializeField] Canvas controllerCanvas;

    // Private fields
    Renderer rend;
    Color startColor;
    Map map;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        map = GameObject.Find("Map").GetComponent<Map>();
        controllerCanvas.gameObject.SetActive(false);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        rend.material.color = highlightColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        map.HideCanvases();
        controllerCanvas.gameObject.SetActive(true);
    }
}
