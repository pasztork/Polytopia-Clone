namespace Model
{
    public class Harbor : WaterTroopTrainingBuilding
    {
        public Harbor(Player player) : base()
        {
            initialValues = BuildingProperties["Harbor"];
            Init(player);
        }

        public override string ToString()
        {
            return "Harbor";
        }
    }
}
