namespace Model
{
    public abstract class TroopTrainingBuilding : BuildingBase
    {
        protected bool troopTrained;

        public TroopTrainingBuilding()
        {
            Requirements = new TrainingRequirementsList();
        }

        public override bool TrainTroop(TroopBase troop)
        {
            if (troopTrained)
                return false;

            troopTrained = true;
            return Tile.TrainTroop(troop);
        }
    }
}
