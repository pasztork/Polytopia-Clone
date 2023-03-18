using System;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    public static HoverManager Instance { get; private set; }

    public event Action<HoverEffect> OnTileClicked;
    public event Action<HoverEffect> OnTileExited;

    public HoverEffect Selected { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one HoverManager in scene!");
            return;
        }
        Instance = this;
    }

    public void AnnounceOnClickEvent(HoverEffect hoverEffect)
    {
        OnTileClicked?.Invoke(hoverEffect);
    }

    public void AnnounceOnExitEvent(HoverEffect hoverEffect)
    {
        OnTileExited?.Invoke(hoverEffect);
    }
}
