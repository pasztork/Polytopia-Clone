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
        [SerializeField] private Color playerColor;

        private void Awake()
        {
            Model.Player player = new Model.Player(name);
            player.Techs = View.TechTreeManager.Instance.GetNewTechTree();
            View.TurnManager.Instance.PlayerColors.Add(player.Name, playerColor);
            player.StartingCityRange = startingCityRange;
            player.AvailableBuildings = startingBuildings.ToList();
            player.AvailableTroops = startingTroops.ToList();
            player.OnStartingCitySpawned += BuildStartingCity;
        }

        private void BuildStartingCity(Model.Player player, Model.TileBase modelTile, Model.BuildingBase modelBuilding)
        {
            View.BuildingManager.Instance.BuildStartingCity(modelTile, modelBuilding, player.Name);
        }
    }
}

