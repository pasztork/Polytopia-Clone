using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using View;

namespace Controller
{
    public class TroopManager : MonoBehaviour
    {
        public static TroopManager Instance { get; private set; }

        public event Action OnTrainAttempted;

        public Dictionary<View.TroopBase, Model.TroopBase> ViewToModelMap { get; }
            = new Dictionary<View.TroopBase, Model.TroopBase>();

        public Dictionary<Model.TroopBase, View.TroopBase> ModelToViewMap { get; }
            = new Dictionary<Model.TroopBase, View.TroopBase>();

        [SerializeField] private SerializableDictionary<string, View.TroopBase> blueprints;
        public SerializableDictionary<string, View.TroopBase> Blueprints { get => blueprints; }

        private View.TroopBase selectedTroop;
        public View.TroopBase SelectedTroop
        {
            get => selectedTroop;
            set
            {
                selectedTroop = value;
                if (selectedTroop != null)
                    View.HighlightManager.Instance.FireMonoBehaviourSelectedEvent(selectedTroop);
            }
        }

        public View.TroopBase Blueprint { private get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one TrainManager in scene!");
                return;
            }
            Instance = this;
        }

        public void Train()
        {
            // Should throw error if there are no subscribers.
            // Whoever responds should set the value of Blueprint.
            OnTrainAttempted.Invoke();
            if (Blueprint == null || BuildingManager.Instance.SelectedBuilding == null)
                return;

            View.BuildingBase building = BuildingManager.Instance.SelectedBuilding;
            Model.BuildingBase modelBuilding =
                BuildingManager.Instance.ViewToModelMap[building];
            View.Tile tile = MapManager.Instance.ModelToViewMap[modelBuilding.Tile];
            View.TroopBase viewTroop = Instantiate(Blueprint,
                tile.transform.position + new Vector3(1f, 1.5f, 1f),
                Quaternion.identity);
            Model.TroopBase troop =
                viewTroop.ToModel(Model.TurnManager.Instance.CurrentPlayer);
            bool trained =
                Model.TrainManager.Instance.Train(modelBuilding, troop);

            if (!trained)
            {
                viewTroop.TakeDamage(0);
                Destroy(viewTroop);
                return;
            }
            NameText troopNameText = viewTroop.GetComponentInChildren<NameText>();
            //troopNameText.Name = Model.TurnManager.Instance.CurrentPlayer.Name;
            troopNameText.BackgroundColor = TurnManager.Instance.PlayerColors[Model.TurnManager.Instance.CurrentPlayer.Name];

            ViewToModelMap[viewTroop] = troop;
            ModelToViewMap[troop] = viewTroop;
            BuildingManager.Instance.SelectedBuilding = null;
            View.HighlightManager.Instance.FireMonoBehaviourSelectedEvent(null);
        }

        public void MoveSelectedTroop(View.Tile tile)
        {
            Model.TroopBase modelTroop = ViewToModelMap[SelectedTroop];
            Model.TileBase modelTile = MapManager.Instance.ViewToModelMap[tile];
            View.Tile from = MapManager.Instance.ModelToViewMap[modelTroop.Tile];

            bool moved = Model.TurnManager.Instance.CurrentPlayer.MoveTroop(modelTroop, modelTile);
            if (!moved)
                return;

            SelectedTroop.Move(from, tile);
            SelectedTroop = null;
            View.HighlightManager.Instance.FireMonoBehaviourSelectedEvent(null);
        }

        public void Attack(View.TroopBase target)
        {
            Model.TroopBase modelAttacker = ViewToModelMap[selectedTroop];
            Model.TroopBase modelTarget = ViewToModelMap[target];

            bool success = Model.TurnManager.Instance.CurrentPlayer.Attack(modelAttacker, modelTarget);
            if (!success)
                return;

            SelectedTroop = null;
            View.HighlightManager.Instance.FireMonoBehaviourSelectedEvent(null);
        }
    }
}