using System;
using System.Collections.Generic;
using UnityEngine;
using View;

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
            if (Blueprint == null || TroopManager.Instance.SelectedTroop == null)
            {
                return;
            }

            Model.TroopBase troop = TroopManager.Instance.ViewToModelMap[TroopManager.Instance.SelectedTroop];
            Model.BuildingBase building = Blueprint.ToModel(Model.TurnManager.Instance.CurrentPlayer);
            bool built = Model.BuildManager.instance.Build(troop, building);

            if (!built)
                return;

            Tile tile = MapManager.Instance.ModelToViewMap[troop.Tile];
            View.BuildingBase viewBuilding = 
                Instantiate(Blueprint, tile.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);

            viewBuilding.GetComponentInChildren<NameText>().BackgroundColor = 
                TurnManager.Instance.PlayerColors[Model.TurnManager.Instance.CurrentPlayer.Name];

            ViewToModelMap[viewBuilding] = building;
            ModelToViewMap[building] = viewBuilding;
        }

        public void BuildStartingCity(Model.TileBase modelTile, Model.BuildingBase modelBuilding, string name)
        {
            View.Tile viewTile = MapManager.Instance.ModelToViewMap[modelTile];
            View.BuildingBase viewBuilding = Instantiate(blueprints["City"],
                    viewTile.transform.position + new Vector3(0f, 1f, 0f),
                    Quaternion.identity);
            NameText buildingText = viewBuilding.GetComponentInChildren<NameText>();
            buildingText.Name = name + "\nCapital";
            buildingText.BackgroundColor = TurnManager.Instance.PlayerColors[name];

            ViewToModelMap[viewBuilding] = modelBuilding;
            ModelToViewMap[modelBuilding] = viewBuilding;
        }
    }
}