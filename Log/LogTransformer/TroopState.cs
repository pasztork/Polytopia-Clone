namespace LogView.LogTransformer
{
    public class TroopState
    {
        public int Health { get; set; } = new int();
        public int Damage { get; set; } = new int();
        public int[] TilesToMove { get; set; } = Array.Empty<int>();
        public List<EnemyState> EnemiesToAttack { get; set; } = new();
        public List<(string, CostState)> BuildingsToBuild { get; set; } = new();
    }
}
