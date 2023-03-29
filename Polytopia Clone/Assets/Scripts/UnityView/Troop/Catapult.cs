namespace View
{
    public class Catapult : OffensiveTroop
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase catapult = new Model.Catapult();
            catapult.OnDamageTaken += TakeDamage;
            catapult.TroopProperty = new Model.TroopProperty(
                troopProperties.Health, troopProperties.Damage, 
                troopProperties.MovementRange, troopProperties.AttackRange);
            catapult.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return catapult;
        }
    }
}