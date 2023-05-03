using UnityEngine;

namespace View
{
    public class Harbor : BuildingBase
    {
        public void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.BuildingBase.BuildingProperties["Harbor"].Health);
            Troops.Add("Boat");
        }
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase harbor = new Model.Harbor(player);
            harbor.OnDamageTaken += TakeDamage;
            return harbor;
        }

        public override bool CanTrain()
        {
            return true;
        }
    }
}
