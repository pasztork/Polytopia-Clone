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
            playerColor = Color.white;
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
            BuildingManager.Instance.BuildStartingCity(tile, city, player.Name);
        }
    }
}

