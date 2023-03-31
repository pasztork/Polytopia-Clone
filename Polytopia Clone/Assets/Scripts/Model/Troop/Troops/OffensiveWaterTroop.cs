namespace Model
{
    public abstract class OffensiveWaterTroop : WaterTroop
    {
        public override void ApplyAllPropertyBonus(Player player)
        {
            base.ApplyAllPropertyBonus(player);
            TroopProperty.Damage += player.BonusProperty.OffensiveDamageBonus;
        }
    }
}