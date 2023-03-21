using UnityEngine;

namespace View
{
    public class BuildingManager : MonoBehaviour
    {
        public static BuildingManager Instance { get; private set; }

        private BuildingBase selectedBuilding;
        public BuildingBase SelectedBuilding
        {
            get => selectedBuilding;
            set
            {
                selectedBuilding = value;
                HighlightManager.Instance.FireMonoBehaviourSelectedEvent(selectedBuilding);
            }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one BuildingManger in scene!");
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            HighlightManager.Instance.OnMonoBehaviourSelected += (monoBehaviour) =>
            {
                if (selectedBuilding == monoBehaviour)
                    return;

                BuildingBase original = selectedBuilding;
                selectedBuilding = null;
                original?.Deselect();
            };
        }
    }
}