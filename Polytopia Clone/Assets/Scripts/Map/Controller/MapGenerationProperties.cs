using UnityEngine;

namespace Controller
{
    [CreateAssetMenu]
    public class MapGenerationProperties : ScriptableObject
    {
        [SerializeField] private int minMountainCount;
        [SerializeField] private int maxMountainCount;
        [SerializeField] private int minForrestCountPerChunk;
        [SerializeField] private int maxForrestCountPerChunk;
        [SerializeField] private float desertChunkProbability;
        [SerializeField] private float waterTileProbability;

        public Model.MapGenerationProperties ToModel()
        {
            return new Model.MapGenerationProperties
            {
                MinMountainCount = minMountainCount,
                MaxMountainCount = maxMountainCount,
                MinForestCountPerChunk = minForrestCountPerChunk,
                MaxForestCountPerChunk = maxForrestCountPerChunk,
                DesertChunkProbability = desertChunkProbability,
                WaterTileProbability = waterTileProbability
            };
        }
    }
}