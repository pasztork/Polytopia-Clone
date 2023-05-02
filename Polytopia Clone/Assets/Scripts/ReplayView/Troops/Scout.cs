using UnityEngine;

namespace ReplayView
{
    public class Scout : OffensiveTroop
    {
        private void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.TroopBase.TroopProperties["Scout"].Health);
            movementRange = Model.TroopBase.TroopProperties["Scout"].MovementRange;
        }

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase scout = new Model.Scout(player);
            scout.TroopProperty.DodgeRate = 0;
            scout.OnDamageTaken += TakeDamage;
            scout.OnTroopHealed += Heal;
            return scout;
        }
    }
}
