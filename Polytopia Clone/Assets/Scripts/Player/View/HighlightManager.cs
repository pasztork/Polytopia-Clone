using System;
using UnityEngine;

namespace View
{
    public class HighlightManager : MonoBehaviour
    {
        public static HighlightManager Instance { get; private set; }

        public event Action<MonoBehaviour> OnMonoBehaviourSelected;

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
            Model.TurnManager.Instance.OnTurnStarted +=
                (player) => FireMonoBehaviourSelectedEvent(null);
        }

        public void FireMonoBehaviourSelectedEvent(MonoBehaviour monoBehaviour)
        {
            OnMonoBehaviourSelected(monoBehaviour);
        }
    }
}