using Controller;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public abstract class BuildingBase : MonoBehaviour
    {
        [Header("Cost Settings")]
        [SerializeField] protected Controller.Cost cost;
        public Controller.Cost Cost { get => cost; }

        [Header("Production Settings")]
        [SerializeField] protected int productionRate;

        [Header("Highlight Settings")]
        [SerializeField] private Color hoverColor;
        private Color startColor;

        public abstract Model.BuildingBase ToModel(Model.Player player);

        private void Awake()
        {
            startColor = GetComponent<Renderer>().material.color;

            HighlightManager.Instance.OnMonoBehaviourSelected += (mono) =>
            {
                if (mono == this)
                    return;

                GetComponent<Renderer>().material.color = startColor;
            };
        }

        private void OnMouseEnter()
        {   
            if (!Model.TurnManager.Instance.CurrentPlayer.Buildings.Contains(BuildingManager.Instance.ViewToModelMap[this]))
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
            if (!Model.TurnManager.Instance.CurrentPlayer.Buildings.Contains(BuildingManager.Instance.ViewToModelMap[this]))
            {
                return;
            }
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            Controller.TroopManager.Instance.SelectedTroop = null;
            Controller.MapManager.Instance.SelectedTile = null;
            Controller.BuildingManager.Instance.SelectedBuilding = this;
        }

        private void OnMouseExit()
        {
            if (!Model.TurnManager.Instance.CurrentPlayer.Buildings.Contains(BuildingManager.Instance.ViewToModelMap[this]))
            {
                return;
            }
            Deselect();
        }

        public void Deselect()
        {
            if (Controller.BuildingManager.Instance.SelectedBuilding != this)
                GetComponent<Renderer>().material.color = startColor;
        }
    }
}