using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class BuildingManager : MonoBehaviour
    {
        public static BuildingManager Instance { get; private set; }

        public event Action OnBuildAttempted;

        public Dictionary<View.BuildingBase, Model.BuildingBase> ViewToModelMap { get; }
            = new Dictionary<View.BuildingBase, Model.BuildingBase>();

        public Dictionary<Model.BuildingBase, View.BuildingBase> ModelToViewMap { get; }
            = new Dictionary<Model.BuildingBase, View.BuildingBase>();

        [SerializeField] private SerializableDictionary<string, View.BuildingBase> blueprints;
        public SerializableDictionary<string, View.BuildingBase> Blueprints { get => blueprints; }

        private View.BuildingBase selectedBuilding;
        public View.BuildingBase SelectedBuilding
        {
            get => selectedBuilding;
            set
            {
                selectedBuilding = value;
                if (selectedBuilding != null)
                    View.HighlightManager.Instance.FireMonoBehaviourSelectedEvent(selectedBuilding);
            }
        }

        public View.BuildingBase Blueprint { private get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one BuildManager in scene!");
                return;
            }
            Instance = this;
        }

        public void Build()
        {
            // Should throw error if there are no subscribers.
            // Whoever responds should set the value of Blueprint.
            OnBuildAttempted.Invoke();
            if (Blueprint == null || MapManager.Instance.SelectedTile == null)
                return;

            View.Tile tile = MapManager.Instance.SelectedTile;
            Model.BuildingBase building =
                Blueprint.ToModel(Model.TurnManager.Instance.CurrentPlayer);
            bool built =
                Model.BuildManager.Instance.Build(
                    MapManager.Instance.ViewToModelMap[tile],
                    building);

            if (!built)
                return;

            View.BuildingBase viewBuilding = Instantiate(Blueprint,
                tile.transform.position + new Vector3(0f, 1f, 0f),
                Quaternion.identity);

            ViewToModelMap[viewBuilding] = building;
            ModelToViewMap[building] = viewBuilding;
        }
    }
}