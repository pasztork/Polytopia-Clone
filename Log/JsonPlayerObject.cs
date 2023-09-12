namespace LogView
{
    public class JsonPlayerObject
    {
        public string Name { get; set; } = string.Empty;
        public float[] Color { get; set; } = Array.Empty<float>();
        public int[] StartingTile { get; set; } = Array.Empty<int>();
        public int StartingCityRange { get; set; } = 1;
    }
}