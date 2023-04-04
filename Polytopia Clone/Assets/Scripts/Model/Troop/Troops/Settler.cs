namespace Model
{
    public class Settler : WorkerTroop
    {
        public Settler(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties.Settler;
            Init(player);
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