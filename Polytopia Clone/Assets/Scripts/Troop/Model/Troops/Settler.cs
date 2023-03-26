namespace Model
{
    public class Settler : TroopBase, LandTroop
    {
        public override void FillRequirements(RequirementsListBase requirements)
        {
            requirements.TrainingBuilderFound = true;
        }
    }
}