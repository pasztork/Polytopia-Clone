using System;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    public static HighlightManager Instance { get; private set; }

    public event Action<MonoBehaviour> MonoBehaviourSelected;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one HighlightManager in scene!");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        TurnManager.Instance.StartTurn +=
            () => FireMonoBehaviourSelectedEvent(null);
    }

    public void FireMonoBehaviourSelectedEvent(MonoBehaviour monoBehaviour) =>
        MonoBehaviourSelected(monoBehaviour);
}
