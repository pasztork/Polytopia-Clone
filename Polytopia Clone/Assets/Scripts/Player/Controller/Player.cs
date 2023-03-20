using System.Linq;
using UnityEngine;

namespace Controller
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private new string name;
        [SerializeField] private string[] startingBuildings;
        [SerializeField] private SerializableDictionary<string, int> baseProduction;
        [SerializeField] private BaseActionCount baseActionCount;

        private void Awake()
        {
            Model.Player player = new Model.Player(name, baseActionCount.CreateDictionary());
            player.ResourceContainer.BaseMoneyProduction = baseProduction["Money"];
            player.ResourceContainer.BaseMaterialProduction = baseProduction["Material"];
            player.ResourceContainer.BaseFoodProduction = baseProduction["Food"];
            player.AvailableBuildings = startingBuildings.ToList();
        }
    }
}

