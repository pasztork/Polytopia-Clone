namespace Model
{
    public class Settler : TroopBase
    {
        public override bool CanBuild(TroopTrainingBuilding building)
        {
            return true;
        }
    }
}