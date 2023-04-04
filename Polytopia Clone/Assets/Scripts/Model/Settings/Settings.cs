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
        public JsonBank Bank { get; set; }
        public JsonCity City { get; set; }
        public JsonFarm Farm { get; set; }
        public JsonHarbor Harbor { get; set; }
        public JsonSupplier Supplier { get; set; }
    }

    public class JsonBank
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int ProductionRate { get; set; }
    }


    public class JsonCity
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int ProductionRate { get; set; }
    }

    public class JsonFarm
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int ProductionRate { get; set; }
    }

    public class JsonHarbor
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int ProductionRate { get; set; }
    }

    public class JsonSupplier
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int ProductionRate { get; set; }
    }

    public class TroopProperties
    {
        public JsonArcher Archer { get; set; }
        public JsonBoat Boat { get; set; }
        public JsonBuilder Builder { get; set; }
        public JsonCatapult Catapult { get; set; }
        public JsonScout Scout { get; set; }
        public JsonSettler Settler { get; set; }
        public JsonWarrior Warrior { get; set; }
    }

    public class JsonArcher
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int MovementRange { get; set; }
        public int AttackRange { get; set; }
        public float DodgeRate { get; set; }
    }

    public class JsonBoat
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int MovementRange { get; set; }
        public int AttackRange { get; set; }
        public float DodgeRate { get; set; }
    }

    public class JsonBuilder
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int MovementRange { get; set; }
        public int AttackRange { get; set; }
        public float DodgeRate { get; set; }
    }

    public class JsonCatapult
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int MovementRange { get; set; }
        public int AttackRange { get; set; }
        public float DodgeRate { get; set; }
    }

    public class JsonScout
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int MovementRange { get; set; }
        public int AttackRange { get; set; }
        public float DodgeRate { get; set; }
    }

    public class JsonSettler
    {
        public JsonCost Cost { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int MovementRange { get; set; }
        public int AttackRange { get; set; }
        public float DodgeRate { get; set; }
    }

    public class JsonWarrior
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
        public JsonArchery Archery { get; set; }
        public JsonBanking Banking { get; set; }
        public JsonCatapultTech Catapult { get; set; }
        public JsonFarming Farming { get; set; }
        public JsonForestry Forestry { get; set; }
        public JsonGemmining GemMining { get; set; }
        public JsonHarborTech Harbor { get; set; }
        public JsonIndustrialrevolution IndustrialRevolution { get; set; }
        public JsonIrrigation Irrigation { get; set; }
        public JsonMathematics Mathematics { get; set; }
        public JsonMilitarism Militarism { get; set; }
        public JsonMining Mining { get; set; }
        public JsonNavigation Navigation { get; set; }
        public JsonRiding Riding { get; set; }
        public JsonSailing Sailing { get; set; }
        public JsonSanitation Sanitation { get; set; }
        public JsonStockmarket StockMarket { get; set; }
        public JsonStrategy Strategy { get; set; }
    }

    public class JsonArchery
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonBanking
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonCatapultTech
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonFarming
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonForestry
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonGemmining
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonHarborTech
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonIndustrialrevolution
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonIrrigation
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonMathematics
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonMilitarism
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonMining
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonNavigation
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonRiding
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonSailing
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonSanitation
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonStockmarket
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }

    public class JsonStrategy
    {
        public int Food { get; set; }
        public int Material { get; set; }
        public int Money { get; set; }
    }
}