using UnityEngine;

namespace View
{
    public class JsonLogManager : MonoBehaviour
    {
        public static JsonLogManager Instance { get; private set; } = null;

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
            JsonLogger.Init();
        }
    }
}