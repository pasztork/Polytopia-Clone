using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public abstract class BuildingBase : MonoBehaviour
    {
        [Header("Cost Settings")]
        [SerializeField] protected View.Cost cost;
        public View.Cost Cost { get => cost; }

        [Header("Production Settings")]
        [SerializeField] protected int productionRate;

        [Header("Building Properties")]
        public View.BuildingProperty buildingProperties;

        [Header("Highlight Settings")]
        private Color hoverColor = Color.yellow;
        private Color selectColor = Color.magenta;
        private Color startColor;

        public abstract Model.BuildingBase ToModel(Model.Player player);

        private void Awake()
        {
            startColor = GetComponent<Renderer>().material.color;
            HighlightManager.Instance.OnMonoBehaviourSelected += DeselectIfNotSelected;
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().Initialize(buildingProperties.Health);
        }

        protected void DeselectIfNotSelected(MonoBehaviour mono)
        {
            if (mono == this)
                return;

            GetComponent<Renderer>().material.color = startColor;
        }

        private void OnMouseEnter()
        {
            if (!Model.DependencyContainer.Get<Model.TurnManagerBase>().CurrentPlayer.Buildings.Contains(View.BuildingManager.Instance.ViewToModelMap[this]))
            {
                return;
            }
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            GetComponent<Renderer>().material.color = hoverColor;
        }

        protected virtual void OnMouseDown()
        {
            if (!Model.DependencyContainer.Get<Model.TurnManagerBase>().CurrentPlayer.Buildings.Contains(View.BuildingManager.Instance.ViewToModelMap[this]))
            {
                if (GetComponent<Renderer>().material.color == selectColor)
                {
                    View.BuildingManager.Instance.Attack(this);
                }
                return;
            }
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            Controller.TroopManager.Instance.SelectedTroop = null;
            View.MapManager.Instance.SelectedTile = null;
            View.BuildingManager.Instance.SelectedBuilding = this;
        }

        private void OnMouseExit()
        {
            if (!Model.DependencyContainer.Get<Model.TurnManagerBase>().CurrentPlayer.Buildings.Contains(View.BuildingManager.Instance.ViewToModelMap[this]))
            {
                return;
            }
            Deselect();
        }

        public void Deselect()
        {
            if (View.BuildingManager.Instance.SelectedBuilding != this)
                GetComponent<Renderer>().material.color = startColor;
        }

        public void TakeDamage(int remainingHealth)
        {
            if (remainingHealth <= 0)
            {
                HighlightManager.Instance.OnMonoBehaviourSelected -= DeselectIfNotSelected;
                Deselect();
                Destroy(gameObject);
                return;
            }

            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().Value = remainingHealth;
        }
    }
}