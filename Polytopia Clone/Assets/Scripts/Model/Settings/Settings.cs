using System.Collections.Generic;

namespace Model
{
    public class Settings
    {
        public JsonProductionRate BaseProduction { get; set; }
        public Dictionary<string, JsonBuilding> BuildingProperties { get; set; }
        public Dictionary<string, JsonTroop> TroopProperties { get; set; }
        public Dictionary<string, JsonCost> TechTreeItemCosts { get; set; }
    }

    public class JsonCost
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
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

    public class JsonTroop
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int MovementRange { get; set; }
        public int AttackRange { get; set; }
        public float DodgeRate { get; set; }
    }
}