namespace View
{
    public class Settler : WorkerTroop
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase settler = new Model.Settler();
            settler.OnDamageTaken += TakeDamage;
            settler.TroopProperty = new Model.TroopProperty(
                TroopProperties.Health, TroopProperties.Damage,
                TroopProperties.MovementRange, TroopProperties.AttackRange, TroopProperties.DodgeRate);
            settler.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return settler;
        }
    }
}
