namespace Model
{
    public class MapGenerationProperties
    {
        public int MinMountainCount { get; set; }
        public int MaxMountainCount { get; set; }
        public int MinForrestCountPerChunk { get; set; }
        public int MaxForrestCountPerChunk { get; set; }
        public float DesertChunkProbability { get; set; }
        public float WaterTileProbability { get; set; }
    }
}