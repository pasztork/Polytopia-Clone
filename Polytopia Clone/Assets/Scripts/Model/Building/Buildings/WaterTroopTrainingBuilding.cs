namespace Model
{
    public abstract class WaterTroopTrainingBuilding : BuildingBase
    {
        protected bool troopTrained;

        public WaterTroopTrainingBuilding()
        {
            Requirements = new WaterTrainingRequirementsList();
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
