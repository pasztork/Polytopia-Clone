using UnityEngine;

namespace ReplayView
{
    public class Farm : BuildingBase
    {
        public void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.BuildingBase.BuildingProperties["Farm"].Health);
        }
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase farm = new Model.Farm(player);
            farm.OnDamageTaken += TakeDamage;
            return farm;
        }
    }
}