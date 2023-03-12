using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour
{
    public Color StartColor { get; private set; }

    [SerializeField] private Color hoverColor;
    private new Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        StartColor = renderer.material.color;
        BuildCanvas.Instance.CanvasMoved += Reset;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Set();
    }

    private void OnMouseExit() { Reset(); }

    private void OnMouseDown()
    {
        Set();
        BuildCanvas.Instance.InvokeEvent();
    }

    private void Set() { renderer.material.color = hoverColor; }

    private void Reset()
    {
        if (BuildManager.Instance.SelectedTile?.gameObject != gameObject && renderer.material.color != StartColor)
        {
            renderer.material.color = StartColor;
        }
    }
}
