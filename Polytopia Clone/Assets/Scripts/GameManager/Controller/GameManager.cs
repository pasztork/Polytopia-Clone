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
            Model.MapGenerator.Instance.MinMountainCount = MapManager.Instance.MinMountainCount;
            Model.MapGenerator.Instance.MaxMountainCount = MapManager.Instance.MaxMountainCount;
            Model.MapGenerator.Instance.MinForrestCountPerChunk = MapManager.Instance.MinForrestCountPerChunk;
            Model.MapGenerator.Instance.MaxForrestCountPerChunk = MapManager.Instance.MaxForrestCountPerChunk;
            Model.MapGenerator.Instance.WaterTileProbability = MapManager.Instance.WaterTileProbability;
            Model.MapGenerator.Instance.DesertChunkProbability = MapManager.Instance.DesertChunkProbability;
        }
    }
}
