using UnityEngine;

namespace ReplayView
{
    public abstract class BuildingBase : MonoBehaviour
    {
        public abstract Model.BuildingBase ToModel(Model.Player player);

        public void TakeDamage(int remainingHealth)
        {
            if (remainingHealth <= 0)
            {
                Destroy(gameObject);
                return;
            }

            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().Value = remainingHealth;
        }
    }
}
