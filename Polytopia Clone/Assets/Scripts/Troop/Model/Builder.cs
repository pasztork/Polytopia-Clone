namespace Model
{
    public class Builder : TroopBase, LandTroop, WaterTroop
    {
        public override bool CanBuild(NonTrainingBuilding building)
        {
            return true;
        }

        public override bool CanBuild(WaterTroopTrainingBuilding building)
        {
            return true;
        }
    }
}