using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    // Parameters
    [SerializeField] Color highlightColor;

    // Private fields
    Renderer rend;
    Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
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

        if (BuildManager.selectedTile == this)
        {
            BuildCanvas.Instance.Hide();
            return;
        }

        BuildCanvas.Instance.MoveToTile(this);
        BuildManager.selectedTile = this;
    }
}
