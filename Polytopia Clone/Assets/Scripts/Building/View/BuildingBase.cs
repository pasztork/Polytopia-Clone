using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public abstract class BuildingBase : MonoBehaviour
    {
        public event Action OnBuildingClicked;

        [Header("Cost Settings")]
        [SerializeField] protected Controller.Cost cost;
        public Controller.Cost Cost { get => cost; }

        [Header("Production Settings")]
        [SerializeField] protected int productionRate;

        [Header("Highlight Settings")]
        [SerializeField] private Color hoverColor;
        private Color StartColor;

        public void FireOnBuildingClickedEvent()
        {
            OnBuildingClicked?.Invoke();
        }

        public abstract Model.BuildingBase ToModel(Model.Player player);

        private void Start()
        {
            StartColor = GetComponent<Renderer>().material.color;
            BuildingManager.Instance.OnBuildingSelected += (building) =>
            {
                if (building == this)
                    return;

                GetComponent<Renderer>().material.color = StartColor;
            };

            Model.TurnManager.Instance.OnTurnStarted += (player) =>
            {
                // Controller.BuildManager.Instance.SelectedBuilding = null;
                GetComponent<Renderer>().material.color = StartColor;
            };

            HighlightManager.Instance.MonoBehaviourSelected += (mono) =>
            {
                if (mono == this)
                    return;

                // Controller.BuildManager.Instance.SelectedBuilding = null;
                GetComponent<Renderer>().material.color = StartColor;
            };
        }

        private void OnMouseEnter()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            GetComponent<Renderer>().material.color = hoverColor;
        }

        protected virtual void OnMouseDown() =>
            Select();

        private void OnMouseExit() =>
            Deselect();

        public void Select()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            if (Controller.BuildManager.Instance.SelectedBuilding == this)
            {
                Controller.BuildManager.Instance.SelectedBuilding = null;
                return;
            }

            Controller.BuildManager.Instance.SelectedBuilding = this;
        }

        public void Deselect()
        {
            if (Controller.BuildManager.Instance.SelectedBuilding != this)
                GetComponent<Renderer>().material.color = StartColor;
        }
    }
}