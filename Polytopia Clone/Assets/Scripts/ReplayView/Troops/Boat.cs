using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReplayView
{
    public class Boat : OffensiveTroop
    {
        private void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.TroopBase.TroopProperties["Boat"].Health);
            movementRange = Model.TroopBase.TroopProperties["Boat"].MovementRange;
        }

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase boat = new Model.Boat(player);
            boat.TroopProperty.DodgeRate = 0;
            boat.OnDamageTaken += TakeDamage;
            boat.OnTroopHealed += Heal;
            return boat;
        }
    }
}
