using UnityEngine;

namespace Controller
{
    [CreateAssetMenu]
    public class MapGenerationProperties : ScriptableObject
    {
        [SerializeField] private int minMountainCount;
        [SerializeField] private int maxMountainCount;
        [SerializeField] private int minForestCountPerChunk;
        [SerializeField] private int maxForestCountPerChunk;
        [SerializeField] private float desertChunkProbability;
        [SerializeField] private float waterTileProbability;

        public Model.MapGenerationProperties ToModel()
        {
            return new Model.MapGenerationProperties
            {
                MinMountainCount = minMountainCount,
                MaxMountainCount = maxMountainCount,
                MinForestCountPerChunk = minForestCountPerChunk,
                MaxForestCountPerChunk = maxForestCountPerChunk,
                DesertChunkProbability = desertChunkProbability,
                WaterTileProbability = waterTileProbability
            };
        }
    }
}