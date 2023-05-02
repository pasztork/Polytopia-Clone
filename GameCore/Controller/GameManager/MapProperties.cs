namespace Controller
{
    public class MapProperties
    {
        public int Size { get; set; } = 16;
        public Model.MapGenerationProperties GenerationProperties { get; set; } = new Model.MapGenerationProperties()
        {
            MinMountainCount = 3,
            MaxMountainCount = 5,
            MinForestCountPerChunk = 3,
            MaxForestCountPerChunk = 5,
            DesertChunkProbability = 0.1f,
            WaterTileProbability = 0.2f
        };
    }
}