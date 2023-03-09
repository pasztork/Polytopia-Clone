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
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        renderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        renderer.material.color = StartColor;
    }
}
