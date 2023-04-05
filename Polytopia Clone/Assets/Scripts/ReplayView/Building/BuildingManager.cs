using System.Collections.Generic;
using UnityEngine;

namespace ReplayView
{
    public class BuildingManager : MonoBehaviour
    {
        private static BuildingManager instance;
        public static BuildingManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<BuildingManager>();
                }
                return instance;
            }
        }

        public Dictionary<BuildingBase, Model.BuildingBase> ViewToModelMap { get; }
            = new Dictionary<BuildingBase, Model.BuildingBase>();

        public Dictionary<Model.BuildingBase, BuildingBase> ModelToViewMap { get; }
            = new Dictionary<Model.BuildingBase, BuildingBase>();

        [SerializeField] private SerializableDictionary<string, BuildingBase> blueprints;
        public SerializableDictionary<string, BuildingBase> Blueprints { get => blueprints; }

        public void Build(Tile where, string what)
        {
            Model.TileBase modelTile = MapManager.Instance.ViewToModelMap[where];
            BuildingBase viewBuilding = Instantiate(Blueprints[what], where.transform.position + new Vector3(0f, where.Offset.y, 0f), Quaternion.identity);
            Model.BuildingBase building = viewBuilding.ToModel(Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer);
            Controller.GameManager.Get<Controller.BuildingManagerBase>().Build(modelTile.TroopOnTop, building);

            viewBuilding.GetComponentInChildren<View.NameText>().BackgroundColor = View.TurnManager.Instance.PlayerColors[Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Name];

            ViewToModelMap[viewBuilding] = building;
            ModelToViewMap[building] = viewBuilding;
        }

        public void BuildStartingCity(Model.TileBase tile, Model.City city, string playerName)
        {
            Tile viewTile = MapManager.Instance.ModelToViewMap[tile];
            BuildingBase viewCity = Instantiate(Blueprints["City"], viewTile.transform.position + new Vector3(0f, viewTile.Offset.y, 0f), Quaternion.identity);
            View.NameText buildingText = viewCity.GetComponentInChildren<View.NameText>();
            buildingText.Name = playerName + "\nCapital";
            buildingText.BackgroundColor = View.TurnManager.Instance.PlayerColors[playerName];
            city.OnDamageTaken += viewCity.TakeDamage;

            ViewToModelMap[viewCity] = city;
            ModelToViewMap[city] = viewCity;
        }

        public void Attack(Tile attackerTile, Tile targetTile)
        {
            Model.TileBase modelAttackerTile = MapManager.Instance.ViewToModelMap[attackerTile];
            Model.TileBase modelTargetTile = MapManager.Instance.ViewToModelMap[targetTile];
            Controller.GameManager.Get<Controller.BuildingManagerBase>().Attack(modelAttackerTile.TroopOnTop, modelTargetTile.BuildingOnTop);
        }
    }
}