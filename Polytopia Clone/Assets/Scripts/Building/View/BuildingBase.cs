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

            HighlightManager.Instance.OnMonoBehaviourSelected += (mono) =>
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

            if (Controller.BuildingManager.Instance.SelectedBuilding == this)
            {
                Controller.BuildingManager.Instance.SelectedBuilding = null;
                return;
            }

            Controller.BuildingManager.Instance.SelectedBuilding = this;
        }

        public void Deselect()
        {
            if (Controller.BuildingManager.Instance.SelectedBuilding != this)
                GetComponent<Renderer>().material.color = StartColor;
        }
    }
}