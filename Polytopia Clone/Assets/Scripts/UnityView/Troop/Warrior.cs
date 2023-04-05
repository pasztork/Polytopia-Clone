using UnityEngine;

namespace View
{
    public class Warrior : OffensiveTroop
    {
        private void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.TroopBase.TroopProperties["Warrior"].Health);
            movementRange = Model.TroopBase.TroopProperties["Warrior"].MovementRange;
        }

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase warrior = new Model.Warrior(player);
            warrior.OnDamageTaken += TakeDamage;
            warrior.OnTroopHealed += Heal;
            return warrior;
        }
    }
}