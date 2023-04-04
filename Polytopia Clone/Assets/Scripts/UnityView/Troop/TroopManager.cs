using System;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class TroopManager : MonoBehaviour
    {
        private static TroopManager instance;
        public static TroopManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<TroopManager>();
                }
                return instance;
            }
        }

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

        public void Train()
        {
            // Should throw error if there are no subscribers.
            // Whoever responds should set the value of Blueprint.
            OnTrainAttempted.Invoke();
            if (Blueprint == null || View.BuildingManager.Instance.SelectedBuilding == null)
                return;

            Model.BuildingBase modelBuilding = View.BuildingManager.Instance.ViewToModelMap[View.BuildingManager.Instance.SelectedBuilding];
            View.Tile tile = View.MapManager.Instance.ModelToViewMap[modelBuilding.Tile];
            View.TroopBase viewTroop = Instantiate(Blueprint, tile.transform.position + tile.Offset, Quaternion.identity);
            Model.TroopBase troop = viewTroop.ToModel(Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer);
            bool trained = Controller.GameManager.Get<Controller.TroopManagerBase>().Train(modelBuilding, troop);

            if (!trained)
            {
                viewTroop.TakeDamage(0);
                Destroy(viewTroop);
                return;
            }
            viewTroop.SetReferenceToProperties(troop);
            viewTroop.GetComponentInChildren<View.NameText>().BackgroundColor = View.TurnManager.Instance.PlayerColors[Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name];

            ViewToModelMap[viewTroop] = troop;
            ModelToViewMap[troop] = viewTroop; 
            View.BuildingManager.Instance.SelectedBuilding = null;
            View.HighlightManager.Instance.FireMonoBehaviourSelectedEvent(null);
        }

        public void MoveSelectedTroop(View.Tile tile)
        {
            Model.TroopBase modelTroop = ViewToModelMap[SelectedTroop];
            Model.TileBase modelTile = View.MapManager.Instance.ViewToModelMap[tile];
            View.Tile from = View.MapManager.Instance.ModelToViewMap[modelTroop.Tile];

            bool moved = Controller.GameManager.Get<Controller.TroopManagerBase>().MoveTroop(modelTroop, modelTile);
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

            bool success = Controller.GameManager.Get<Controller.TroopManagerBase>().Attack(modelAttacker, modelTarget);
            if (!success)
                return;

            SelectedTroop = null;
            View.HighlightManager.Instance.FireMonoBehaviourSelectedEvent(null);
        }
    }
}