namespace Model
{
    public class Builder : WorkerTroop
    {
        public Builder(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Builder"];
            Init(player);
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