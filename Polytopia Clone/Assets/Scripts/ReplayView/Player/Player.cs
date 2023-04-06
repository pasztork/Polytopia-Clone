using LogView;
using Model;
using UnityEngine;

namespace ReplayView
{
    public class Player : MonoBehaviour
    {
        private Color playerColor;

        public void SetPlayerFromLog(JsonPlayerObject player)
        {
            Model.Player modelPlayer = new Model.Player(player.Name);
            modelPlayer.StartingCityRange = player.StartingCityRange;
            playerColor = Color.white;
            TurnManager.Instance.SetPlayerColor(player.Name, playerColor);
            Model.City city = new Model.City(modelPlayer);
            TileBase tile = Model.GameManager.Get<Model.MapManagerBase>().Tiles[player.StartingTile[0], player.StartingTile[1]];
            BuildingManager.Instance.BuildStartingCity(tile, city, player.Name);
        }
    }
}

