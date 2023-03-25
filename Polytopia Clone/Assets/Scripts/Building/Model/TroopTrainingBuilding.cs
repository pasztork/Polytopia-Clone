namespace Model
{
    public class TroopTrainingBuilding : BuildingBase
    {
        protected bool troopTrained;
        public override bool TrainTroop(TroopBase troop)
        {
            if (troopTrained)
                return false;

            troopTrained = true;
            return Tile.TrainTroop(troop);
        }
    }
}
