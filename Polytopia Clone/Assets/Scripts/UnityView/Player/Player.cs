using LogView;
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

        public void SetDefaultParams(JsonPlayerObject param)
        {
            name = param.Name;
            startingCityRange = param.StartingParams.StartingCityRange;
            startingBuildings = param.StartingParams.StartingBuildings;
            startingTroops = param.StartingParams.StartingTroops;
            DictionaryWrapper wrapper = new DictionaryWrapper();
            foreach (var p in param.StartingParams.BaseProduction)
                wrapper.AddKeyValue(p.Key, p.Value);
            baseProduction = wrapper;
            Color szin = new Color();
            szin.r = (float)param.StartingParams.PlayerColor[0];
            szin.g = (float)param.StartingParams.PlayerColor[1];
            szin.b = (float)param.StartingParams.PlayerColor[2];
            szin.a = (float)param.StartingParams.PlayerColor[3];
            playerColor = szin;
        }
    }
}

