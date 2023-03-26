namespace Model
{
    public class Settler : TroopBase, LandTroop
    {
        public override bool CanBuild(TroopTrainingBuilding building)
        {
            return true;
        }
    }
}