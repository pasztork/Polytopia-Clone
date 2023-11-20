using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    public class PlayerTransferer : MonoBehaviour
    {
        public static PlayerTransferer Instance { get; private set; }

        public List<string> PlayerNames {  get; set; }
        public List<Color> PlayerColors {  get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void TransferAndStart()
        {
            SceneManager.LoadScene(1);
        }
    }
}
