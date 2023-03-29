using System;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class BuildingManager : MonoBehaviour
    {
        public static BuildingManager Instance { get; private set; }

        // Used by UI elements
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
            if (Blueprint == null || View.TroopManager.Instance.SelectedTroop == null)
            {
                return;
            }

            Model.TroopBase troop = View.TroopManager.Instance.ViewToModelMap[View.TroopManager.Instance.SelectedTroop];
            View.Tile tile = View.MapManager.Instance.ModelToViewMap[troop.Tile];
            View.BuildingBase viewBuilding = Instantiate(Blueprint, tile.transform.position + new Vector3(0f, tile.Offset.y, 0f), Quaternion.identity);
            Model.BuildingBase building = viewBuilding.ToModel(Model.DependencyContainer.Get<Model.TurnManagerBase>().CurrentPlayer);
            bool built = Controller.GameManager.Get<Controller.BuildingManagerBase>().Build(troop, building);

            if (!built)
            {
                viewBuilding.TakeDamage(0);
                Destroy(viewBuilding);
                return;
            }

            viewBuilding.GetComponentInChildren<View.NameText>().BackgroundColor = View.TurnManager.Instance.PlayerColors[Model.DependencyContainer.Get<Model.TurnManagerBase>().CurrentPlayer.Name];

            ViewToModelMap[viewBuilding] = building;
            ModelToViewMap[building] = viewBuilding;
            View.TroopManager.Instance.SelectedTroop = null;
            View.HighlightManager.Instance.FireMonoBehaviourSelectedEvent(null);
        }

        public void BuildStartingCity(Model.TileBase modelTile, Model.BuildingBase modelBuilding, string name)
        {
            View.Tile viewTile = View.MapManager.Instance.ModelToViewMap[modelTile];
            View.BuildingBase viewBuilding = Instantiate(blueprints["City"], viewTile.transform.position + new Vector3(0f, viewTile.Offset.y, 0f), Quaternion.identity);
            View.NameText buildingText = viewBuilding.GetComponentInChildren<View.NameText>();
            buildingText.Name = name + "\nCapital";
            buildingText.BackgroundColor = View.TurnManager.Instance.PlayerColors[name];
            modelBuilding.BuildingProperty = new Model.BuildingProperty(viewBuilding.buildingProperties.Health);
            modelBuilding.OnDamageTaken += viewBuilding.TakeDamage;

            ViewToModelMap[viewBuilding] = modelBuilding;
            ModelToViewMap[modelBuilding] = viewBuilding;
        }

        public void Attack(View.BuildingBase building)
        {
            Model.TroopBase modelAttacker = View.TroopManager.Instance.ViewToModelMap[View.TroopManager.Instance.SelectedTroop];
            Model.BuildingBase modelTarget = ViewToModelMap[building];
            Controller.GameManager.Get<Controller.BuildingManager>().Attack(modelAttacker, modelTarget);
        }
    }
}