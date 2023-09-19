using UnityEngine;

namespace ReplayView
{
    public abstract class TroopBase : MonoBehaviour
    {
        protected int movementRange = 0;

        public abstract Model.TroopBase ToModel(Model.Player player);

        public void Move(Tile to)
        {
            gameObject.transform.position = to.gameObject.transform.position + to.Offset;
        }

        public void Heal(int remainingHealth)
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().Value = remainingHealth;
        }

        public virtual void TakeDamage(int remainingHealth)
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