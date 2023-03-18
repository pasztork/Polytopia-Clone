using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour
{
    public Color StartColor { get; set; }

    [SerializeField] private Color hoverColor;
    private new Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        StartColor = renderer.material.color;
    }

    private void Start()
    {
        TurnManager.Instance.StartTurn += Reset;
        HoverManager.Instance.OnTileClicked += (hoverEffect) =>
        {
            if (hoverEffect == this)
            {
                Set();
                return;
            }
            Reset();

        };
        HoverManager.Instance.OnTileExited += (hoverEffect) =>
        {
            if (HoverManager.Instance.Selected != this && hoverEffect == this)
                Reset();
        };
        Plane.Instance.OnPlaneClick += Reset;
    }

    private void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            Set();
    }

    private void OnMouseExit()
    {
        HoverManager.Instance.AnnounceOnExitEvent(this);
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            HoverManager.Instance.AnnounceOnClickEvent(this);
            HoverManager.Instance.Selected = this;
        }
    }

    private void Set()
    {
        renderer.material.color += hoverColor;
    }

    private void Reset()
    {
        renderer.material.color = StartColor;
    }
}
