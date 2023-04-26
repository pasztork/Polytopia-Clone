namespace LogView.LogTransformer
{

    public class GameState
    {
        public List<PlayerState> PlayerState { get; set; } = new();
    }

    public class PlayerState
    {
        public string Name { get; set; }
        public List<int[]> Banks { get; set; } = new();
        public List<int[]> Cities { get; set; } = new();
        public List<int[]> Farms { get; set; } = new();
        public List<int[]> Harbors { get; set; } = new();
        public List<int[]> Supplier { get; set; } = new();
        public List<int[]> Archers { get; set; } = new();
        public List<int[]> Boats { get; set; } = new();
        public List<int[]> Builders { get; set; } = new();
        public List<int[]> Catapults { get; set; } = new();
        public List<int[]> Scouts { get; set; } = new();
        public List<int[]> Settlers { get; set; } = new();
        public List<int[]> Warriors { get; set; } = new();
        public List<string[]> Techs { get; set; } = new();
    }

}
