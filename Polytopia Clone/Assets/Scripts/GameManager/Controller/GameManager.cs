using UnityEngine;

namespace Controller
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            CreateMap();
            Model.MapManager.Instance.GenerateMap();
        }

        private void Start()
        {
            Model.GameManager.Instance.Start();
        }

        private void CreateMap()
        {
            Model.MapManager.Instance.Size = MapManager.Instance.Size;
            Model.MapManager.Instance.WaterProbability = MapManager.Instance.WaterProbability;
            Model.MapManager.Instance.MinMountainCount = MapManager.Instance.MinMountainCount;
            Model.MapManager.Instance.MaxMountainCount = MapManager.Instance.MaxMountainCount;
        }
    }
}
