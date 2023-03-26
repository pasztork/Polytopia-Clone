namespace Model
{
    public class Builder : TroopBase, LandTroop, WaterTroop
    {
        public override void FillRequirements(RequirementsListBase requirements)
        {
            requirements.NonTrainingBuilderFound = true;
            requirements.WaterBuilderFound = true;
        }
    }
}