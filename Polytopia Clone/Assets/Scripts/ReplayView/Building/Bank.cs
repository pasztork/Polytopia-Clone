using UnityEngine;

namespace ReplayView
{
    public class Bank : BuildingBase
    {
        public void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.BuildingBase.BuildingProperties["Bank"].Health);
        }
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase bank = new Model.Bank(player);
            bank.OnDamageTaken += TakeDamage;
            return bank;
        }
    }
}
