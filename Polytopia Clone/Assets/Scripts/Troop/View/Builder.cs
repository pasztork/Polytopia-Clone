using UnityEngine;

namespace View
{
    public class Builder : TroopBase
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase builder = new Model.Builder();
            builder.OnDamageTaken += TakeDamage;
            builder.TroopProperty = new Model.TroopProperty(
                troopProperties.Health, troopProperties.Damage,
                troopProperties.MovementRange, troopProperties.AttackRange);
            builder.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return builder;
        }
    }
}
