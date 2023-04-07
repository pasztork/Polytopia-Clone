using System;
using System.Collections.Generic;
using UnityEngine;

namespace ReplayView
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

        public Dictionary<TroopBase, Model.TroopBase> ViewToModelMap { get; }
            = new Dictionary<TroopBase, Model.TroopBase>();

        public Dictionary<Model.TroopBase, TroopBase> ModelToViewMap { get; }
            = new Dictionary<Model.TroopBase, TroopBase>();

        [SerializeField] private SerializableDictionary<string, TroopBase> blueprints;
        public SerializableDictionary<string, TroopBase> Blueprints { get => blueprints; }

        public void Train(Tile where, string what)
        {
            TroopBase viewTroop = Instantiate(Blueprints[what], where.transform.position + where.Offset, Quaternion.identity);
            Model.TroopBase modelTroop = viewTroop.ToModel(Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer);
            Model.TileBase tileBase = MapManager.Instance.ViewToModelMap[where];
            Controller.GameManager.Get<Controller.TroopManagerBase>().Train(tileBase.BuildingOnTop, modelTroop);

            viewTroop.GetComponentInChildren<View.NameText>().BackgroundColor = TurnManager.Instance.PlayerColors[Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name];

            ViewToModelMap[viewTroop] = modelTroop;
            ModelToViewMap[modelTroop] = viewTroop;
        }

        public void MoveSelectedTroop(Tile from, Tile to)
        {
            Model.TileBase modelFrom = MapManager.Instance.ViewToModelMap[from];
            Model.TroopBase modelTroop = modelFrom.TroopOnTop;
            Model.TileBase modelTo = MapManager.Instance.ViewToModelMap[to];

            Controller.GameManager.Get<Controller.TroopManagerBase>().MoveTroop(modelTroop, modelTo);
            ModelToViewMap[modelTroop].Move(to);
        }

        public void Attack(Tile attackerTile, Tile targetTile)
        {
            Model.TileBase modelAttackerTile = MapManager.Instance.ViewToModelMap[attackerTile];
            Model.TileBase modelTargetTile = MapManager.Instance.ViewToModelMap[targetTile];

            Controller.GameManager.Get<Controller.TroopManagerBase>().Attack(modelAttackerTile.TroopOnTop, modelTargetTile.TroopOnTop);
        }
    }
}