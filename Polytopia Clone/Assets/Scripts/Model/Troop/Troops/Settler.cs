namespace Model
{
    public class Settler : WorkerTroop
    {
        public override void FillRequirements(RequirementsListBase requirements)
        {
            requirements.TrainingBuilderFound = true;
        }

        public override bool Relocate(RockTile target)
        {
            return MoveTo(target);
        }

        public override string ToString()
        {
            return "Settler";
        }
    }
}