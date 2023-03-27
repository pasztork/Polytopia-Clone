namespace Model
{
    public class Settler : TroopBase, LandTroop, WaterTroop
    {
        public override void FillRequirements(RequirementsListBase requirements)
        {
            requirements.TrainingBuilderFound = true;
        }
    }
}