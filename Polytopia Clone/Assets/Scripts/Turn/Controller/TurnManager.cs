using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager Instance { get; private set; }

        [SerializeField] private BaseActionCount baseActionCount;
        public BaseActionCount BaseActionCount { get => baseActionCount; }

        public Dictionary<string, int> CurrentPossibleActions { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one TurnManager in scene!");
                return;
            }
            Instance = this;
        }

        public void FinishTurn() =>
            Model.TurnManager.Instance.FinishTurn();
    }
}