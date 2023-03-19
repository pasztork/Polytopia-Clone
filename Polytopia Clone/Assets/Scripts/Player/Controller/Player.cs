using UnityEngine;

namespace Controller
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private new string name;

        private void Start()
        {
            TurnManager.Instance.OnCreated += () =>
            {
                Model.Player player = new Model.Player();
                player.Name = name;
            };
        }
    }
}

