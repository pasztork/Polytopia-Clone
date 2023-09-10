namespace LogView.LogTransformer
{
    public class BuildingState
    {
        public int Health { get; set; } = new int();
        public List<(string, CostState)> TroopsToTrain { get; set; } = new();
    }
}
