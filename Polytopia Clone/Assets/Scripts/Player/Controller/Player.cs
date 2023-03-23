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
        [SerializeField] private DictionaryWrapper baseActionCount;

        private void Start()
        {
            Model.Player player = new Model.Player(name);
            player.StartingCityRange = startingCityRange;
            player.StartingProduction = baseProduction.CreateDictionary();
            player.ActionCount = baseActionCount.CreateDictionary();
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

