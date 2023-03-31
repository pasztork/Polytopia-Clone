namespace View
{
    public class Scout : OffensiveTroop
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase scout = new Model.Scout();
            scout.OnDamageTaken += TakeDamage;
            scout.TroopProperty = new Model.TroopProperty(TroopProperties.Health, TroopProperties.Damage, TroopProperties.MovementRange, TroopProperties.AttackRange, TroopProperties.DodgeRate);
            scout.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return scout;
        }
    }
}
