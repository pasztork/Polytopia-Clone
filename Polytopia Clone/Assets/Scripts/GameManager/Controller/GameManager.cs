using UnityEngine;

namespace Controller
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            SetupGeneration();
            Model.MapManager.Instance.GenerateMap();
        }

        private void Start()
        {
            Model.GameManager.Instance.Start();
        }

        private void SetupGeneration()
        {
            Model.MapManager.Instance.Size = MapManager.Instance.Size;
            Model.MapGenerator.Instance.MGP = MapManager.Instance.GenerationProperties.ToModel();
        }
    }
}
