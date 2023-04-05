using UnityEngine;

namespace View
{
    public class JsonLogManager : MonoBehaviour
    {
        public static JsonLogManager Instance { get; private set; } = null;
        [SerializeField]
        private bool needToLog;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one JsonLogManager in scene!");
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            if (needToLog)
                LogView.JsonLogger.Init();
        }
    }
}