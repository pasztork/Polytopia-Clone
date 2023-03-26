namespace Model
{
    public class RockTile : NonTraversableTile
    {
        public override string ToString() => "Rock";

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
    }
}