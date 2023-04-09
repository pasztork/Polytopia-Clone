using System.Collections.Generic;
using UnityEngine;

namespace ReplayView
{
    public class Player : MonoBehaviour
    {
        private Color playerColor;

        public void SetPlayerFromLog(LogView.JsonPlayerObject player)
        {
            Model.Player modelPlayer = new Model.Player(player.Name);
            modelPlayer.StartingCityRange = player.StartingCityRange;
            playerColor = new Color(player.Color[0], player.Color[1], player.Color[2], player.Color[3]);
            TurnManager.Instance.SetPlayerColor(player.Name, playerColor);
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
    }
}

