using System.Linq;
using UnityEngine;

namespace Controller
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
            TurnManager.Instance.PlayerColors.Add(player.Name, playerColor);
            player.StartingCityRange = startingCityRange;
            player.StartingProduction = baseProduction.CreateDictionary();
            player.AvailableBuildings = startingBuildings.ToList();
            player.AvailableTroops = startingTroops.ToList();

            player.OnStartingCitySpawned += BuildStartingCity;
        }

        private void BuildStartingCity(Model.Player player, Model.TileBase modelTile, Model.BuildingBase modelBuilding)
        {
            BuildingManager.Instance.BuildStartingCity(modelTile, modelBuilding, player.Name);
        }
    }
}

