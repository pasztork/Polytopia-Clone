using System;
using UnityEngine;

namespace View
{
    public class TroopManager : MonoBehaviour
    {
        public static TroopManager Instance { get; private set; }

        public event Action<TroopBase> OnTroopSelected;

        private TroopBase selectedTroop;
        public TroopBase SelectedTroop
        {
            get => selectedTroop;
            set
            {
                selectedTroop = value;
                HighlightManager.Instance.FireMonoBehaviourSelectedEvent(selectedTroop);
                OnTroopSelected?.Invoke(selectedTroop);
            }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one TroopManager in scene!");
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            HighlightManager.Instance.MonoBehaviourSelected += (monoBehaviour) =>
            {
                if (selectedTroop == monoBehaviour)
                    return;

                TroopBase original = selectedTroop;
                selectedTroop = null;
                original?.Deselect();
            };
        }
    }
}
