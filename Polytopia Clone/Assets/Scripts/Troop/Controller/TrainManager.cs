using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class TrainManager : MonoBehaviour
    {
        public static TrainManager Instance { get; private set; }

        public event Action OnTrainAttempted;

        public Dictionary<View.TroopBase, Model.TroopBase> ViewToModelMap { get; }
            = new Dictionary<View.TroopBase, Model.TroopBase>();

        public Dictionary<Model.TroopBase, View.TroopBase> ModelToViewMap { get; }
            = new Dictionary<Model.TroopBase, View.TroopBase>();

        [SerializeField] private SerializableDictionary<string, View.TroopBase> blueprints;
        public SerializableDictionary<string, View.TroopBase> Blueprints { get => blueprints; }

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
            if (Blueprint == null)
                return;

            View.BuildingBase building = BuildManager.Instance.SelectedBuilding;
            Model.TroopBase troop =
                Blueprint.ToModel(Model.TurnManager.Instance.CurrentPlayer);
            Model.BuildingBase modelBuilding =
                BuildManager.Instance.ViewToModelMap[building];
            bool trained =
                Model.TrainManager.Instance.Train(modelBuilding, troop);

            if (!trained)
                return;

            View.Tile tile = MapManager.Instance.ModelToViewMap[modelBuilding.Tile];
            View.TroopBase viewTroop = Instantiate(Blueprint,
                tile.transform.position + new Vector3(1f, 1.5f, 1f),
                Quaternion.identity);

            ViewToModelMap[viewTroop] = troop;
            ModelToViewMap[troop] = viewTroop;
        }
    }
}