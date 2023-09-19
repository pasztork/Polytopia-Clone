namespace Model
{
    public abstract class WaterTroopTrainingBuilding : BuildingBase
    {
        public override bool TroopTrainedInTurn { get; protected set; }

        public WaterTroopTrainingBuilding()
        {
            Requirements = new WaterTrainingRequirementsList();
            TrainableTroops.Add("Boat");
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

        public override bool CheckTechRequirement(GrassTile tile, Player player)
        {
            return false;
        }

        public override bool CheckTechRequirement(ForestTile tile, Player player)
        {
            return false;
        }

        public override bool CheckTechRequirement(SandTile tile, Player player)
        {
            return false;
        }

        public override bool CheckTechRequirement(WaterTile tile, Player player)
        {
            return true;
        }
    }
}
