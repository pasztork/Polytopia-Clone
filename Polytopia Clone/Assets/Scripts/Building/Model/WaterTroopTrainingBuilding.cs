namespace Model
{
    public abstract class WaterTroopTrainingBuilding : BuildingBase
    {
        protected bool troopTrained;
        public override bool TrainTroop(TroopBase troop)
        {
            if (troopTrained)
                return false;

            if(troop is WaterTroop)
            {
                troopTrained = true;
                return Tile.TrainTroop(troop);
            }
            return false;
        }
    }
}
