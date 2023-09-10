namespace LogView.LogTransformer
{
    public class EnemyState
    {
        public string Name { get; set; } = String.Empty;
        public int[] Coordinates { get; set; } = new int[2];
        public string Type { get; set; } = String.Empty;
        public int Health { get; set; } = new int();
        public int Damage { get; set; } = new int();
    }
}
