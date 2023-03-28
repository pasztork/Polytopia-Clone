using UnityEngine;

namespace Controller
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            SetupGeneration();
            Model.DependencyContainer.Get<Model.MapManagerBase>().GenerateMap();
        }

        private void Start()
        {
            Model.DependencyContainer.Get<Model.GameManagerBase>().Start();
        }

        private void SetupGeneration()
        {
            Model.DependencyContainer.Get<Model.MapManagerBase>().Size = MapManager.Instance.Size;
            Model.DependencyContainer.Get<Model.MapGeneratorBase>().MGP = MapManager.Instance.GenerationProperties.ToModel();
        }
    }
}
