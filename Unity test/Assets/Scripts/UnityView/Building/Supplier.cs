using UnityEngine;

namespace View
{
    public class Supplier : BuildingBase
    {
        public void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.BuildingBase.BuildingProperties["Supplier"].Health);
        }
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase supplier = new Model.Supplier(player);
            supplier.OnDamageTaken += TakeDamage;
            return supplier;
        }
    }
}