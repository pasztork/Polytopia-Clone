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

        public bool Train(Tile where, string what)
        {
            TroopBase viewTroop = Instantiate(Blueprints[what], where.transform.position + where.Offset, Quaternion.identity);
            Model.TroopBase modelTroop = viewTroop.ToModel(Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer);
            Model.TileBase tileBase = MapManager.Instance.ViewToModelMap[where];
            bool trained = Controller.GameManager.Get<Controller.TroopManagerBase>().Train(tileBase.BuildingOnTop, modelTroop);

            viewTroop.GetComponentInChildren<View.NameText>().BackgroundColor = TurnManager.Instance.PlayerColors[Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name];

            ViewToModelMap[viewTroop] = modelTroop;
            ModelToViewMap[modelTroop] = viewTroop;

            return trained;
        }

        public bool MoveSelectedTroop(Model.TroopBase troop, Model.TileBase to)
        {
            Tile viewTo = MapManager.Instance.ModelToViewMap[to];

            bool moved = Controller.GameManager.Get<Controller.TroopManagerBase>().MoveTroop(troop, to);

            ModelToViewMap[troop].Move(viewTo);

            return moved;
        }

        public bool Attack(Model.TroopBase attacker, Model.TroopBase target)
        {
            return Controller.GameManager.Get<Controller.TroopManagerBase>().Attack(attacker, target);
        }
    }
}