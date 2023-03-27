namespace Model
{
    public class WaterTile : NonTraversableTile
    {
        public override string ToString()
        {
            return "Water";
        }

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

        public override bool TrainTroop(TroopBase troop)
        {
            if (TroopOnTop != null)
                return false;

            bool success = troop.Train(this);
            if (!success)
                return false;

            troop.Tile.TroopOnTop = null;
            TroopOnTop = troop;
            return true;
        }

        public override bool AcceptTroop(TroopBase troop)
        {
            if (TroopOnTop != null)
                return false;

            bool success = troop.Relocate(this);
            if (!success)
                return false;

            TroopOnTop = troop;
            return true;
        }
    }
}