using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class Plane : MonoBehaviour
    {
        public static Plane Instance { get; private set; }

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
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                View.MapManager.Instance.SelectedTile = null;
                View.BuildingManager.Instance.SelectedBuilding = null;
                View.TroopManager.Instance.SelectedTroop = null;
                HighlightManager.Instance.FireMonoBehaviourSelectedEvent(this);
            }
        }
    }
}