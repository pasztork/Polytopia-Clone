using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public abstract class TroopBase : MonoBehaviour
    {
        public event Action OnTroopClicked;

        [Header("Cost Settings")]
        [SerializeField] protected Controller.Cost cost;
        public Controller.Cost Cost { get => cost; }

        [Header("Troop Properties")]
        public Controller.TroopProperty troopProperties;

        [Header("Highlight Settings")]
        [SerializeField] private Color hoverColor;
        private Color StartColor;

        public void FireOnTroopClickedEvent()
        {
            OnTroopClicked?.Invoke();
        }

        public abstract Model.TroopBase ToModel(Model.Player player);

        private void Start()
        {
            StartColor = GetComponent<Renderer>().material.color;
            TroopManager.Instance.OnTroopSelected += (troop) =>
            {
                if (troop == this)
                    return;

                GetComponent<Renderer>().material.color = StartColor;
            };

            Model.TurnManager.Instance.OnTurnStarted += (player) =>
            {
                // Controller.TrainManager.Instance.SelectedTroop = null;
                GetComponent<Renderer>().material.color = StartColor;
            };

            HighlightManager.Instance.MonoBehaviourSelected += (mono) =>
            {
                if (mono == this)
                    return;

                // Controller.TrainManager.Instance.SelectedTroop = null;
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

        protected virtual void OnMouseDown()
        {
            Select();
        }

        private void OnMouseExit()
        {
            Deselect();
        }

        public void Select()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            if (TroopManager.Instance.SelectedTroop == this)
            {
                TroopManager.Instance.SelectedTroop = null;
                return;
            }

            TroopManager.Instance.SelectedTroop = this;
        }

        public void Deselect()
        {
            if (TroopManager.Instance.SelectedTroop != this)
                GetComponent<Renderer>().material.color = StartColor;
        }
    }
}