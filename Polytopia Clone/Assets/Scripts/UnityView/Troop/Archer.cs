using UnityEngine;

namespace View
{
    public class Archer : OffensiveTroop
    {
        private void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.TroopBase.TroopProperties.Archer.Health);
            movementRange = Model.TroopBase.TroopProperties.Archer.MovementRange;
        }

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase archer = new Model.Archer(player);
            archer.OnDamageTaken += TakeDamage;
            archer.OnTroopHealed += Heal;
            return archer;
        }
    }
}