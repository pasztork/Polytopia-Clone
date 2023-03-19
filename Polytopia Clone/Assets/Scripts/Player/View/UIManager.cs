using UnityEngine;

namespace View
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("More than one UIManager in scene!");
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            Model.TurnManager.Instance.OnTurnStarted += WrappedHideAll;
            Model.BuildManager.Instance.OnBuildingBuilt += WrappedHideAll;
            Plane.Instance.OnPlaneClick += HideAll;
        }

        private void WrappedHideAll(Model.Player player) => HideAll();

        public void HideAll()
        {
            Controller.MapManager.Instance.SelectedTile = null;
        }
    }
}