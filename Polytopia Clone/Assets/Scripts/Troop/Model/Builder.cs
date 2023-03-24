namespace Model
{
    public class Builder : TroopBase
    {
        public override bool CanBuild(NonTrainingBuilding building)
        {
            return true;
        }
    }
}