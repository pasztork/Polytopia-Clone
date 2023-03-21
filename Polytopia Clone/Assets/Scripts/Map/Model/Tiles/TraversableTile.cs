namespace Model
{
    public abstract class TraversableTile : TileBase
    {
        public override bool SetBuildingOnTop(BuildingBase buildingOnTop)
        {
            if (BuildingOnTop != null)
                return false;

            BuildingOnTop = buildingOnTop;
            return true;
        }

        public override bool TrainTroop(TroopBase troop)
        {
            if (TroopOnTop != null)
                return false;

            TroopOnTop = troop;
            return true;
        }

        public override bool AcceptTroop(TroopBase troop)
        {
            if (TroopOnTop != null)
                return false;

            troop.Tile.TroopOnTop = null;
            TroopOnTop = troop;
            return true;
        }
    }
}