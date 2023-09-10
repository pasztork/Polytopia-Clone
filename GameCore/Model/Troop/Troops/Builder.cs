namespace Model
{
    public class Builder : WorkerTroop
    {
        public Builder(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Builder"];
            Buildings.AddRange(new List<string>() { "Farm", "Harbor", "Supplier", "Bank" });
            Init(player);
        }

        public Builder() : base()
        {
            initialValues = TroopProperties["Builder"];
            Init(GameManager.Get<TurnManagerBase>().CurrentPlayer);
        }

        public override void FillRequirements(RequirementsListBase requirements)
        {
            requirements.NonTrainingBuilderFound = true;
            requirements.WaterBuilderFound = true;
        }

        public override string ToString()
        {
            return "Builder";
        }
    }
}