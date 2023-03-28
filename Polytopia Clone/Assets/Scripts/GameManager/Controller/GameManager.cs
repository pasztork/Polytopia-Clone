using UnityEngine;

namespace Controller
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            SetupGeneration();
            Model.DependencyContainer.Get<Model.MapManager>().GenerateMap();
        }

        private void Start()
        {
            Model.DependencyContainer.Get<Model.GameManager>().Start();
        }

        private void SetupGeneration()
        {
            Model.DependencyContainer.Get<Model.MapManager>().Size = MapManager.Instance.Size;
            Model.DependencyContainer.Get<Model.MapGenerator>().MGP = MapManager.Instance.GenerationProperties.ToModel();
        }
    }
}
