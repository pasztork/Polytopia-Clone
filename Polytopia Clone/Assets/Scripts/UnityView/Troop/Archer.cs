namespace View
{
    public class Archer : OffensiveTroop
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase archer = new Model.Archer();
            archer.OnDamageTaken += TakeDamage;
            archer.TroopProperty = new Model.TroopProperty(
                troopProperties.Health, troopProperties.Damage,
                troopProperties.MovementRange, troopProperties.AttackRange);
            archer.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return archer;
        }
    }
}