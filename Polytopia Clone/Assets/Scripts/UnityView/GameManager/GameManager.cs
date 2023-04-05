using UnityEngine;

namespace View
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Controller.GameManager.NewGame();
        }

        private void Start()
        {
            Controller.GameManager.StartNew();
        }
    }
}
