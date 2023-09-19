namespace Model
{
    public abstract class NonTrainingBuilding : BuildingBase
    {
        public override bool TroopTrainedInTurn {
            get => true; // Always true so it can not train
            protected set { } 
        }

        public NonTrainingBuilding()
        {
            Requirements = new NonTrainingRequirementsList();
        }
    }
}
