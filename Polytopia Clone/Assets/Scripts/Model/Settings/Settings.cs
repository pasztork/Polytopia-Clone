namespace Model
{
    public class Settings
    {
        public BaseProduction BaseProduction { get; set; }
        public BuildingProperties BuildingProperties { get; set; }
        public TroopProperties TroopProperties { get; set; }
        public TechTreeItemCosts TechTreeItemCosts { get; set; }
    }

    public class BaseProduction
    {
        public int Money { get; set; }
        public int Material { get; set; }
        public int Food { get; set; }
    }

    public class JsonCost
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class BuildingProperties
    {
        public JsonBuilding Bank { get; set; }
        public JsonBuilding City { get; set; }
        public JsonBuilding Farm { get; set; }
        public JsonBuilding Harbor { get; set; }
        public JsonBuilding Supplier { get; set; }
    }

    public class JsonBuilding
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int Range { get; set; }
        public JsonProductionRate ProductionRate { get; set; }
    }

    public class JsonProductionRate
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class TroopProperties
    {
        public JsonTroop Archer { get; set; }
        public JsonTroop Boat { get; set; }
        public JsonTroop Builder { get; set; }
        public JsonTroop Catapult { get; set; }
        public JsonTroop Scout { get; set; }
        public JsonTroop Settler { get; set; }
        public JsonTroop Warrior { get; set; }
    }

    public class JsonTroop
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int MovementRange { get; set; }
        public int AttackRange { get; set; }
        public float DodgeRate { get; set; }
    }

    public class TechTreeItemCosts
    {
        public JsonCost Archery { get; set; }
        public JsonCost Banking { get; set; }
        public JsonCost Catapult { get; set; }
        public JsonCost Farming { get; set; }
        public JsonCost Forestry { get; set; }
        public JsonCost GemMining { get; set; }
        public JsonCost Harbor { get; set; }
        public JsonCost IndustrialRevolution { get; set; }
        public JsonCost Irrigation { get; set; }
        public JsonCost Mathematics { get; set; }
        public JsonCost Militarism { get; set; }
        public JsonCost Mining { get; set; }
        public JsonCost Navigation { get; set; }
        public JsonCost Riding { get; set; }
        public JsonCost Sailing { get; set; }
        public JsonCost Sanitation { get; set; }
        public JsonCost StockMarket { get; set; }
        public JsonCost Strategy { get; set; }
    }
}