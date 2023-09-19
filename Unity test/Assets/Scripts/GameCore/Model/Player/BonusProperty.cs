namespace Model
{
    public class BonusProperty
    {
        public int OffensiveLandMoveBonus { get; set; } = 0;
        public int WaterMoveBonus { get; set; } = 0;
        public int OffensiveDamageBonus { get; set; } = 0;
        public double DodgeBonus { get; set; } = 0.0;
        public int HealAmount { get; set; } = 0;
        public int FarmProductionBonus { get; set; } = 0;
        public int BankProductionBonus { get; set; } = 0;
        public int SupplierProductionBonus { get; set; } = 0;
        public float BuildingDiscount { get; set; } = 0f;
    }
}
