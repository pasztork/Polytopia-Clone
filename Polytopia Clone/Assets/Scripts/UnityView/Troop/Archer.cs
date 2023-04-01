namespace View
{
    public class Archer : OffensiveTroop
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase archer = new Model.Archer();
            archer.OnDamageTaken += TakeDamage;
            archer.OnTroopHealed += Heal;
            archer.MaxHealth = TroopProperties.Health;
            archer.TroopProperty = new Model.TroopProperty(
                TroopProperties.Health, TroopProperties.Damage,
                TroopProperties.MovementRange, TroopProperties.AttackRange, TroopProperties.DodgeRate);
            archer.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return archer;
        }
    }
}