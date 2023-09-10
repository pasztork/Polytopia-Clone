namespace Model
{
    public class Settler : WorkerTroop
    {
        public Settler(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Settler"];
            Buildings.AddRange(new List<string>() { "City" });
            Init(player);
        }

        public Settler() : base()
        {
            initialValues = TroopProperties["Settler"];
            Init(GameManager.Get<TurnManagerBase>().CurrentPlayer);
        }

        public override void FillRequirements(RequirementsListBase requirements)
        {
            requirements.TrainingBuilderFound = true;
        }

        public override string ToString()
        {
            return "Settler";
        }
    }
}