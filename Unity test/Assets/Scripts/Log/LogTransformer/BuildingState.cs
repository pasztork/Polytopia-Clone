using System;
using System.Collections.Generic;

namespace LogView.LogTransformer
{
    public class BuildingState
    {
        public string Type { get; set; } = string.Empty;
        public int Health { get; set; } = new int();
        public int[] Position { get; set; } = Array.Empty<int>();
        public List<TrainableTroopState> TroopsToTrain { get; set; } = new();
    }

    public class TrainableTroopState
    {
        public string Type { get; set; } = String.Empty;
        public CostState Cost { get; set; } = new();
    }
}
