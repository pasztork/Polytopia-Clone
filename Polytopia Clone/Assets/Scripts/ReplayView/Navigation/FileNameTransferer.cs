using UnityEngine;

namespace Assets.Scripts.ReplayView.Navigation
{
    public class FileNameTransferer : MonoBehaviour
    {
        public static FileNameTransferer Instance { get; private set; }

        public string LogFilePath { get; set; } = string.Empty;

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
    }
}
