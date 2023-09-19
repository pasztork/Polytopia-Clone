using UnityEngine;

namespace View
{
    public class Scout : OffensiveTroop
    {
        private void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.TroopBase.TroopProperties["Scout"].Health);
        }

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase scout = new Model.Scout(player);
            TroopProperties = scout.TroopProperty;
            scout.OnDamageTaken += TakeDamage;
            scout.OnTroopHealed += Heal;
            return scout;
        }
    }
}
