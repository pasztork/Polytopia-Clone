namespace Model
{
    public abstract class TroopTrainingBuilding : BuildingBase
    {
        protected bool troopTrained = false;

        public TroopTrainingBuilding()
        {
            Requirements = new TrainingRequirementsList();
            TurnManager.Instance.OnTurnStarted +=
                (player) => troopTrained = false;
        }

        public override bool TrainTroop(TroopBase troop)
        {
            if (troopTrained)
                return false;

            bool tileAccepted = Tile.TrainTroop(troop);
            troopTrained = tileAccepted;
            return tileAccepted;
        }
    }
}
