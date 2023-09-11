namespace Model
{
    public abstract class WaterTroopTrainingBuilding : BuildingBase
    {
        public override bool TroopTrained { get; protected set; }

        public WaterTroopTrainingBuilding()
        {
            Requirements = new WaterTrainingRequirementsList();
            TrainableTroops.Add("Boat");
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
