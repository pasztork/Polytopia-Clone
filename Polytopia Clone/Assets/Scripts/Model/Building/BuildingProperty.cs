namespace Model
{
    public class BuildingProperty
    {
        public int Health { get; set; }
        public int ProductionRate { get; set; }

        public BuildingProperty(int health, int productionRate)
        {
            Health = health;
            ProductionRate = productionRate;
        }
    }
}
