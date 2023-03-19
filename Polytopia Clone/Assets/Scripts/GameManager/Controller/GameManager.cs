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
            SetupTurnManager();
            Model.GameManager.Instance.Start();
        }

        private void CreateMap()
        {
            Model.MapManager.Instance.Size = MapManager.Instance.Size;
            Model.MapManager.Instance.WaterProbabilty = MapManager.Instance.WaterProbability;
        }

        private void SetupTurnManager()
        {
            Model.TurnManager.Instance.BaseActionCount =
                TurnManager.Instance.BaseActionCount.CreateDictionary();
        }
    }
}
