namespace Model
{
    public class Supplier : NonTrainingBuilding
    {
        public Supplier(Player player) : base()
        {
            initialValues = BuildingProperties["Supplier"];
            Init(player);
        }

        public override void IncreaseMaterialProduction(int amount)
        {
            foreach (ProducerBase producer in Producers)
            {
                producer.IncreaseProduction(amount);
            }
        }

        public override bool CheckTechRequirement(RockTile tile, Player player)
        {
            if (player.Techs["Mining"].TechTreeItemProperty.IsUnlocked)
            {
                return true;
            }
            return false;
        }

        public override bool CheckTechRequirement(SandTile tile, Player player)
        {
            if (player.Techs["GemMining"].TechTreeItemProperty.IsUnlocked)
            {
                return true;
            }
            return false;
        }

        public override bool CheckTechRequirement(ForestTile tile, Player player)
        {
            if (player.Techs["Forestry"].TechTreeItemProperty.IsUnlocked)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "Supplier";
        }
    }
}