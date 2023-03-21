namespace View
{
    public class Warrior : TroopBase
    {
        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            FireOnTroopClickedEvent();
        }

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase warrior = new Model.Warrior();
            warrior.OnDied += () => TroopManager.Instance.Kill(this);
            warrior.TroopProperty = new Model.TroopProperty(
                troopProperties.Health, troopProperties.Damage,
                troopProperties.MovementRange, troopProperties.AttackRange);
            warrior.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return warrior;
        }
    }
}