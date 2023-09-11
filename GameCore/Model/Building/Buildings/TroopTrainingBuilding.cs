namespace Model
{
    public abstract class TroopTrainingBuilding : BuildingBase
    {
        public override bool TroopTrained { get; protected set; }

        public TroopTrainingBuilding()
        {
            Requirements = new TrainingRequirementsList();
            TrainableTroops.AddRange(new List<string>() { "Archer", "Builder", "Catapult", "Scout", "Settler", "Warrior" });
            GameManager.Get<TurnManagerBase>().OnTurnStarted +=
                (player) => TroopTrained = false;
        }

        public override bool TrainTroop(TroopBase troop)
        {
            if (TroopTrained)
                return false;

            bool tileAccepted = Tile.TrainTroop(troop);
            TroopTrained = tileAccepted;
            return tileAccepted;
        }
    }
}
