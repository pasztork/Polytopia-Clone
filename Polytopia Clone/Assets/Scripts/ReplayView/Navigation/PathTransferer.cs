using UnityEngine;

namespace Assets.Scripts.ReplayView.Navigation
{
    public class PathTransferer : MonoBehaviour
    {
        public static PathTransferer Instance { get; private set; }

        [SerializeField]
        private string _projectRoot;

        public string ProjectRoot { get => _projectRoot; }
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
