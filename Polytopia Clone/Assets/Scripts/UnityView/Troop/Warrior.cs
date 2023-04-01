namespace View
{
    public class Warrior : OffensiveTroop
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase warrior = new Model.Warrior();
            warrior.OnDamageTaken += TakeDamage;
            warrior.OnTroopHealed += Heal;
            warrior.MaxHealth = TroopProperties.Health;
            warrior.TroopProperty = new Model.TroopProperty(TroopProperties.Health, TroopProperties.Damage, TroopProperties.MovementRange, TroopProperties.AttackRange, TroopProperties.DodgeRate);
            warrior.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return warrior;
        }
    }
}