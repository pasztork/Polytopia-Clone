using System;
using System.Collections.Generic;

namespace LogView.LogTransformer
{
    public class TroopState
    {
        public string Type { get; set; } = string.Empty;
        public int Health { get; set; } = new int();
        public int Damage { get; set; } = new int();
        public int[] Position { get; set; } = Array.Empty<int>();
        public List<int[]> TilesToMove { get; set; } = new();
        public List<EnemyState> TroopsToAttack { get; set; } = new();
        public List<EnemyState> BuildingsToAttack { get; set; } = new();
        public List<BuildableBuildingState> BuildingsToBuild { get; set; } = new();
    }

    public class BuildableBuildingState
    {
        public string Type { get; set; } = String.Empty;
        public CostState Cost { get; set; } = new();
    }
}
