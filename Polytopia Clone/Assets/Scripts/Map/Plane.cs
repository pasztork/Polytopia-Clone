using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plane : MonoBehaviour
{
    public static Plane Instance { get; private set; }

    public event Action OnPlaneClick;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Plane in scene!");
            return;
        }
        Instance = this;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        OnPlaneClick?.Invoke();
    }
}
