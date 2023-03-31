namespace Model
{
    public class Farm : NonTrainingBuilding
    {
        public override void IncreaseFoodProduction(int amount)
        {
            foreach (ProducerBase producer in Producers)
            {
                producer.IncreaseProduction(amount);
            }
        }

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
