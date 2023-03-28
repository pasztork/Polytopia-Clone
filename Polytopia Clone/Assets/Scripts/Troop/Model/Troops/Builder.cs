namespace Model
{
    public class Builder : WorkerTroop
    {
        public override void FillRequirements(RequirementsListBase requirements)
        {
            requirements.NonTrainingBuilderFound = true;
            requirements.WaterBuilderFound = true;
        }

        public override bool Relocate(RockTile target)
        {
            return MoveTo(target);
        }

        public override string ToString()
        {
            return "Builder";
        }
    }
}