namespace Model
{
    public class Harbor : WaterTroopTrainingBuilding
    {
        public Harbor(Player player) : base()
        {
            initialValues = BuildingProperties["Harbor"];
            Init(player);
        }

        public Harbor() : base()
        {
            initialValues = BuildingProperties["Harbor"];
            Init(GameManager.Get<TurnManagerBase>().CurrentPlayer);
        }

        public override string ToString()
        {
            return "Harbor";
        }
    }
}
