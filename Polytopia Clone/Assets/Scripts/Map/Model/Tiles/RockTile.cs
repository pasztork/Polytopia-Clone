namespace Model
{
    public class RockTile : NonTraversableTile
    {
        public override string ToString()
        {
            return "Rock";
        }

        public override bool SetBuildingOnTop(BuildingBase buildingOnTop)
        {
            if (BuildingOnTop != null)
                return false;

            if (buildingOnTop is Supplier)
            {
                BuildingOnTop = buildingOnTop;
                return true;
            }
            return false;
        }

        public override bool AcceptTroop(TroopBase troop)
        {
            if (TroopOnTop != null)
                return false;

            bool moved = troop.Relocate(this);
            if (!moved)
                return false;

            TroopOnTop = troop;
            return true;
        }
    }
}