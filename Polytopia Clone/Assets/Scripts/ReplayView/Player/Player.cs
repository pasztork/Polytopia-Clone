using System.Collections.Generic;
using UnityEngine;

namespace ReplayView
{
    public class Player : MonoBehaviour
    {
        private static readonly IList<Color> availableColors = new List<Color>
        {
            Color.red, Color.green, Color.blue, Color.yellow
        };

        private Color playerColor;

        public void SetPlayerFromLog(LogView.JsonPlayerObject player)
        {
            Model.Player modelPlayer = new Model.Player(player.Name);
            modelPlayer.StartingCityRange = player.StartingCityRange;
            SetPlayerColor(player.Name);
            Model.City city = new Model.City(modelPlayer);
            city.Producers.Add(new Model.FoodProducer(modelPlayer.ResourceContainer, Model.Player.BaseProduction.Food));
            city.Producers.Add(new Model.MaterialProducer(modelPlayer.ResourceContainer, Model.Player.BaseProduction.Material));
            city.Producers.Add(new Model.MoneyProducer(modelPlayer.ResourceContainer, Model.Player.BaseProduction.Money));
            Model.TileBase tile = Model.GameManager.Get<Model.MapManagerBase>().Tiles[player.StartingTile[0], player.StartingTile[1]];
            city.Tile = tile;
            city.Player = modelPlayer;
            tile.SetBuildingOnTop(city, modelPlayer);
            modelPlayer.AvailableTiles.Add(tile);
            modelPlayer.AddBuilding(city);
            modelPlayer.Techs = TechTreeManager.Instance.GetNewTechTree();
            modelPlayer.AvailableBuildings = new List<string>();
            modelPlayer.AvailableTroops = new List<string>();
            BuildingManager.Instance.BuildStartingCity(tile, city, player.Name);
        }

        private void SetPlayerColor(string playerName)
        {
            var randomColor = availableColors[Random.Range(0, availableColors.Count)];
            availableColors.Remove(randomColor);
            TurnManager.Instance.SetPlayerColor(playerName, randomColor);
        }
    }
}

