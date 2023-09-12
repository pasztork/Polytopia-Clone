namespace LogView.LogTransformer
{
    public class ActionState
    {
        public string Type { get; } = nameof(ActionState);
        public List<TroopState> Troops { get; set; } = new();
        public List<BuildingState> Buildings { get; set; } = new();
        public List<TechState> TechsToUnlock { get; set; } = new();

    }
}
