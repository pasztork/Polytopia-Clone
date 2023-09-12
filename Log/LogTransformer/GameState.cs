namespace LogView.LogTransformer
{
    public class GameState
    {
        public string Type { get; } = nameof(GameState);
        public List<PlayerState> PlayerState { get; set; } = new();
    }
}
