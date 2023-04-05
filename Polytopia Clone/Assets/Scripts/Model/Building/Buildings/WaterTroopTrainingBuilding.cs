namespace Model
{
    public abstract class WaterTroopTrainingBuilding : BuildingBase
    {
        protected bool troopTrained;

        public WaterTroopTrainingBuilding()
        {
            Requirements = new WaterTrainingRequirementsList();
            GameManager.Get<TurnManagerBase>().OnTurnStarted +=
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
