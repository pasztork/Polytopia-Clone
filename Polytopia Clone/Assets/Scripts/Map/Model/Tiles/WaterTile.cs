namespace Model
{
    public class WaterTile : NonTraversableTile
    {
        public override string ToString() => "Water";

        public override bool SetBuildingOnTop(BuildingBase buildingOnTop)
        {
            if (BuildingOnTop != null)
                return false;

            if (buildingOnTop is WaterTroopTrainingBuilding || buildingOnTop is Supplier)
            {
                BuildingOnTop = buildingOnTop;
                return true;
            }
            return false;
        }
    }
}