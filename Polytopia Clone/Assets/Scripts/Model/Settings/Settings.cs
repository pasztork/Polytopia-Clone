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
        public JsonTechTreeItem Archery { get; set; }
        public JsonTechTreeItem Banking { get; set; }
        public JsonTechTreeItem Catapult { get; set; }
        public JsonTechTreeItem Farming { get; set; }
        public JsonTechTreeItem Forestry { get; set; }
        public JsonTechTreeItem GemMining { get; set; }
        public JsonTechTreeItem Harbor { get; set; }
        public JsonTechTreeItem IndustrialRevolution { get; set; }
        public JsonTechTreeItem Irrigation { get; set; }
        public JsonTechTreeItem Mathematics { get; set; }
        public JsonTechTreeItem Militarism { get; set; }
        public JsonTechTreeItem Mining { get; set; }
        public JsonTechTreeItem Navigation { get; set; }
        public JsonTechTreeItem Riding { get; set; }
        public JsonTechTreeItem Sailing { get; set; }
        public JsonTechTreeItem Sanitation { get; set; }
        public JsonTechTreeItem StockMarket { get; set; }
        public JsonTechTreeItem Strategy { get; set; }
    }

    public class JsonTechTreeItem
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }
}