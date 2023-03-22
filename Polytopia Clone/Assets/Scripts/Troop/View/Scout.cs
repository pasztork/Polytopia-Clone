namespace View
{
    public class Scout : TroopBase
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase scout = new Model.Scout();
            scout.OnDamageTaken += TakeDamage;
            scout.TroopProperty = new Model.TroopProperty(
                troopProperties.Health, troopProperties.Damage,
                troopProperties.MovementRange, troopProperties.AttackRange);
            scout.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return scout;
        }
    }
}
