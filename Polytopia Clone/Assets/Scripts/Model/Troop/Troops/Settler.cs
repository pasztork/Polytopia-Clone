namespace Model
{
    public class Settler : WorkerTroop
    {
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