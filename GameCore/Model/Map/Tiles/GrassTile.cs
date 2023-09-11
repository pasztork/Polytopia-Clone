namespace Model
{
    public class GrassTile : TraversableTile
    {
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

        public override string ToString()
        {
            return "Grass";
        }
    }
}
