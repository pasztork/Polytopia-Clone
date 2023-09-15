namespace Model
{
    public abstract class TroopTrainingBuilding : BuildingBase
    {
        public override bool TroopTrainedInTurn { get; protected set; }

        public TroopTrainingBuilding()
        {
            Requirements = new TrainingRequirementsList();
            TrainableTroops.AddRange(new List<string>() { "Archer", "Builder", "Catapult", "Scout", "Settler", "Warrior" });
            GameManager.Get<TurnManagerBase>().OnTurnStarted +=
                (player) => TroopTrainedInTurn = false;
        }

        public override bool TrainTroop(TroopBase troop)
        {
            if (TroopTrainedInTurn)
                return false;

            bool tileAccepted = Tile.TrainTroop(troop);
            TroopTrainedInTurn = tileAccepted;
            return tileAccepted;
        }
    }
}
