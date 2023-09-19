using UnityEngine;

namespace ReplayView
{
    public class Plane : MonoBehaviour
    {
        public static Plane Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one Plane in scene!");
                return;
            }
            Instance = this;
        }
    }
}
