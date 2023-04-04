using LogView;
using Model;
using System.Linq;
using UnityEngine;

namespace View
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private new string name;
        [SerializeField] private int startingCityRange;
        [SerializeField] private string[] startingBuildings;
        [SerializeField] private string[] startingTroops;
        [SerializeField] private DictionaryWrapper baseProduction;
        [SerializeField] private Color playerColor;

        private void Start()
        {
            Model.Player player = new Model.Player(name);
            player.Techs = View.TechTreeManager.Instance.GetNewTechTree();
            View.TurnManager.Instance.PlayerColors.Add(player.Name, playerColor);
            player.StartingCityRange = startingCityRange;
            player.StartingProduction = baseProduction.CreateDictionary();
            player.AvailableBuildings = startingBuildings.ToList();
            player.AvailableTroops = startingTroops.ToList();
            player.OnStartingCitySpawned += BuildStartingCity;
        }

        private void BuildStartingCity(Model.Player player, Model.TileBase modelTile, Model.BuildingBase modelBuilding)
        {
            View.BuildingManager.Instance.BuildStartingCity(modelTile, modelBuilding, player.Name);
        }

        public void SetPlayerFromLog(JsonPlayerObject player)
        {
            name = player.Name;
            startingCityRange = player.StartingCityRange;
            startingBuildings = null;
            startingTroops = null;
            playerColor = Color.white;
            baseProduction = Model.PropertiesLoader.SetPlayerProperties();
            Model.City city = new Model.City(player.StartingCityRange);
            TileBase tile = Model.GameManager.Get<Model.MapManagerBase>().Tiles[player.StartingTile[0], player.StartingTile[1]];
            View.BuildingManager.Instance.BuildStartingCity( tile, city, player.Name);
        }
    }
}

