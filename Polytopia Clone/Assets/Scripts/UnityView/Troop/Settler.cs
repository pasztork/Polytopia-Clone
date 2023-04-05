using UnityEngine;

namespace View
{
    public class Settler : WorkerTroop
    {
        private void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.TroopBase.TroopProperties["Settler"].Health);
            movementRange = Model.TroopBase.TroopProperties["Settler"].MovementRange;
        }

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase settler = new Model.Settler(player);
            settler.OnDamageTaken += TakeDamage;
            settler.OnTroopHealed += Heal;
            return settler;
        }
    }
}
