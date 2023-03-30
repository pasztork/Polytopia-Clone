namespace Model
{
    public abstract class NonTrainingBuilding : BuildingBase
    {
        public NonTrainingBuilding()
        {
            Requirements = new NonTrainingRequirementsList();
        }
    }
}
