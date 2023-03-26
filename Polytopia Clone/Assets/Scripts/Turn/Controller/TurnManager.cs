using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class TurnManager : MonoBehaviour
    {
        public static TurnManager Instance { get; private set; }

        public Dictionary<string, Color> PlayerColors { get; private set; } = new Dictionary<string, Color>();

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one TurnManager in scene!");
                return;
            }
            Instance = this;
        }

        public void FinishTurn()
        {
            Model.TurnManager.Instance.FinishTurn();
        }
    }
}