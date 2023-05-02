using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReplayView
{
    public class Builder : WorkerTroop
    {
        private void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.TroopBase.TroopProperties["Builder"].Health);
            movementRange = Model.TroopBase.TroopProperties["Builder"].MovementRange;
        }

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase builder = new Model.Builder(player);
            builder.TroopProperty.DodgeRate = 0;
            builder.OnDamageTaken += TakeDamage;
            builder.OnTroopHealed += Heal;
            return builder;
        }

        public override void TakeDamage(int remainingHealth)
        {
            if (remainingHealth <= 0)
            {
                Destroy(gameObject);
                return;
            }

            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().Value = remainingHealth;
        }
    }
}
