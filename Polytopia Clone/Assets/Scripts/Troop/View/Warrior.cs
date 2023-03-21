namespace View
{
    public class Warrior : TroopBase
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase warrior = new Model.Warrior();
            warrior.OnDamageTaken += Kill;
            warrior.TroopProperty = new Model.TroopProperty(
                troopProperties.Health, troopProperties.Damage,
                troopProperties.MovementRange, troopProperties.AttackRange);
            warrior.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return warrior;
        }
    }
}