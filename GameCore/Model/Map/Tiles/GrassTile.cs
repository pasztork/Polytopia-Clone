namespace Model
{
    public class GrassTile : TraversableTile
    {
        public override bool SetBuildingOnTop(BuildingBase buildingOnTop, Player player)
        {
            if (BuildingOnTop != null)
                return false;

            bool techRequirementMet = buildingOnTop.CheckTechRequirement(this, player);
            if (!techRequirementMet)
                return false;

            BuildingOnTop = buildingOnTop;
            return true;
        }

        public override string ToString()
        {
            return "Grass";
        }
    }
}
