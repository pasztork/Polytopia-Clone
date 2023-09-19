namespace Model
{
    public class RockTile : NonTraversableTile
    {
        public override string ToString()
        {
            return "Rock";
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