using UnityEngine;

namespace View
{
    public class Catapult : OffensiveTroop
    {
        private void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.TroopBase.TroopProperties["Catapult"].Health);
            movementRange = Model.TroopBase.TroopProperties["Catapult"].MovementRange;
        }

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase catapult = new Model.Catapult(player);
            catapult.OnDamageTaken += TakeDamage;
            catapult.OnTroopHealed += Heal;
            return catapult;
        }
    }
}