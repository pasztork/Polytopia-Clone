using UnityEngine;

namespace Controller
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private new string name;
        [SerializeField] private SerializableDictionary<string, int> baseProduction;

        private void Start()
        {
            Model.Player player = new Model.Player { Name = name };
            player.ResourceContainer.BaseMoneyProduction = baseProduction["Money"];
            player.ResourceContainer.BaseMaterialProduction = baseProduction["Material"];
            player.ResourceContainer.BaseFoodProduction = baseProduction["Food"];
        }
    }
}

