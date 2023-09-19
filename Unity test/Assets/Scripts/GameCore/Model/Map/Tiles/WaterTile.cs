namespace Model
{
    public class WaterTile : NonTraversableTile
    {
        public override string ToString()
        {
            return "Water";
        }

        public override bool SetBuildingOnTop(BuildingBase buildingOnTop, Player player)
        {
            if (BuildingOnTop != null)
                return false;

            bool techRequirementMet = CheckTechRequirement(buildingOnTop, player);
            if (!techRequirementMet)
                return false;

            BuildingOnTop = buildingOnTop;
            return true;
        }

        public override bool CheckTechRequirement(BuildingBase buildingOnTop, Player player)
        {
            return buildingOnTop.CheckTechRequirement(this, player);
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