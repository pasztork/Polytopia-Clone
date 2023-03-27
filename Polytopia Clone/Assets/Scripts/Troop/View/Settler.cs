namespace View
{
    public class Settler : WorkerTroop
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase settler = new Model.Settler();
            settler.OnDamageTaken += TakeDamage;
            settler.TroopProperty = new Model.TroopProperty(
                troopProperties.Health, troopProperties.Damage,
                troopProperties.MovementRange, troopProperties.AttackRange);
            settler.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return settler;
        }
    }
}
