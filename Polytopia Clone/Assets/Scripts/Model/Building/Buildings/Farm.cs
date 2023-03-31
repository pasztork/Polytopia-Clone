namespace Model
{
    public class Farm : NonTrainingBuilding
    {
        public override bool CheckTechRequirement(SandTile tile, Player player)
        {
            return false;
        }

        public override string ToString()
        {
            return "Farm";
        }
    }
}
