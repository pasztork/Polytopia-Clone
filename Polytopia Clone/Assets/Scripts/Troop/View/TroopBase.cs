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
        [SerializeField] private Color selectColor;
        private Color startColor;

        public void FireOnTroopClickedEvent() =>
            OnTroopClicked?.Invoke();

        public abstract Model.TroopBase ToModel(Model.Player player);

        private void Start()
        {
            startColor = GetComponent<Renderer>().material.color;

            HighlightManager.Instance.OnMonoBehaviourSelected += (mono) =>
            {
                if (mono == this)
                    return;

                // Controller.TrainManager.Instance.SelectedTroop = null;
                GetComponent<Renderer>().material.color = startColor;
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
            TroopBase selectedTroop = Controller.TroopManager.Instance.SelectedTroop;
            if (selectedTroop != this && selectedTroop != null)
            {
                Controller.TroopManager.Instance.Attack(this);
                return;
            }

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

            if (Controller.TroopManager.Instance.SelectedTroop == this)
            {
                Controller.TroopManager.Instance.SelectedTroop = null;
                return;
            }

            Controller.TroopManager.Instance.SelectedTroop = this;
        }

        public void Deselect()
        {
            if (Controller.TroopManager.Instance.SelectedTroop != this)
                GetComponent<Renderer>().material.color = startColor;
        }

        public void Move(Tile tile)
        {
            transform.position = tile.transform.position + new Vector3(1f, 1.5f, 1f);
        }
    }
}