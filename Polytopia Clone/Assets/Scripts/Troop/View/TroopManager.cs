using UnityEngine;

namespace View
{
    public class TroopManager : MonoBehaviour
    {
        public static TroopManager Instance { get; private set; }

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
            HighlightManager.Instance.OnMonoBehaviourSelected += (monoBehaviour) =>
            {
                TroopBase troop = Controller.TroopManager.Instance.SelectedTroop;
                if (troop == monoBehaviour)
                    return;

                TroopBase original = troop;
                troop = null;
                original?.Deselect();
            };
        }

        public void Kill(TroopBase troop)
        {
            DestroyImmediate(troop.gameObject, true);
        }
    }
}
