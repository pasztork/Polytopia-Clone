using System.Linq;
using UnityEngine;

namespace View
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public new string name;
        [SerializeField] private int startingCityRange;
        [SerializeField] private string[] startingBuildings;
        [SerializeField] private string[] startingTroops;
        [SerializeField] public Color playerColor;

        public void SetupPlayer() 
        {
            Model.Player player = new Model.Player(name);
            // player.Techs = View.TechTreeManager.Instance.GetNewTechTree();
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

