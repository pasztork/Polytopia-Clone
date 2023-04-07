using UnityEngine;

namespace View
{
    public class City : BuildingBase
    {
        public void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.BuildingBase.BuildingProperties["City"].Health);
        }
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase city = new Model.City(player);
            city.OnDamageTaken += TakeDamage;
            return city;
        }
    }
}