using System;

namespace LogView.LogTransformer
{
    public class EnemyState
    {
        public string Name { get; set; } = String.Empty;
        public int[] Position { get; set; } = Array.Empty<int>();
        public string Type { get; set; } = String.Empty;
        public int Health { get; set; } = new int();
        public int Damage { get; set; } = new int();
    }
}
