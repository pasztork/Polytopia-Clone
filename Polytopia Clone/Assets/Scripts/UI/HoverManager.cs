using System;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    public static HoverManager Instance { get; private set; }

    public event Action<HoverEffect> OnClicked;
    public event Action<HoverEffect> OnExited;

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

    public void AnnounceOnClickedEvent(HoverEffect hoverEffect)
    {
        OnClicked?.Invoke(hoverEffect);
    }

    public void AnnounceOnExitedEvent(HoverEffect hoverEffect)
    {
        OnExited?.Invoke(hoverEffect);
    }
}
