using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class City : BuildingBase
    {
        public void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.BuildingBase.BuildingProperties["City"].Health);
            Troops.AddRange(new List<string>() { "Archer", "Builder", "Catapult", "Scout", "Settler", "Warrior" });
        }
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase city = new Model.City(player);
            city.OnDamageTaken += TakeDamage;
            return city;
        }

        public override bool CanTrain()
        {
            return true;
        }
    }
}