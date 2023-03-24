namespace Model
{
    public class MapGenerationProperties
    {
        public int MinMountainCount { get; set; }
        public int MaxMountainCount { get; set; }
        public int MinForestCountPerChunk { get; set; }
        public int MaxForestCountPerChunk { get; set; }
        public float DesertChunkProbability { get; set; }
        public float WaterTileProbability { get; set; }
    }
}