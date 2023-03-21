namespace Model
{
    public abstract class TraversableTile : TileBase
    {
        public override bool SetBuildingOnTop(BuildingBase buildingOnTop)
        {
            BuildingOnTop = buildingOnTop;
            return true;
        }

        public override bool TrainTroop(TroopBase troop)
        {
            TroopOnTop = troop;
            return true;
        }
    }
}