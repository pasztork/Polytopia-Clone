using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public abstract class TroopBase : MonoBehaviour
    {
        [Header("Cost Settings")]
        [SerializeField] protected Controller.Cost cost;
        public Controller.Cost Cost { get => cost; }

        [Header("Troop Properties")]
        public Controller.TroopProperty troopProperties;

        [Header("Highlight Settings")]
        [SerializeField] private Color hoverColor;
        [SerializeField] private Color selectColor;
        private Color startColor;

        public abstract Model.TroopBase ToModel(Model.Player player);

        private void Awake()
        {
            startColor = GetComponent<Renderer>().material.color;
            HighlightManager.Instance.OnMonoBehaviourSelected += DeselectIfNotSelected;
        }

        protected void DeselectIfNotSelected(MonoBehaviour mono)
        {
            if (mono == this)
                return;

            GetComponent<Renderer>().material.color = startColor;
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
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            TroopBase selectedTroop = Controller.TroopManager.Instance.SelectedTroop;
            if (selectedTroop != this && selectedTroop != null)
            {
                Controller.TroopManager.Instance.Attack(this);
                return;
            }

            Controller.MapManager.Instance.SelectedTile = null;
            Controller.BuildingManager.Instance.SelectedBuilding = null;
            Controller.TroopManager.Instance.SelectedTroop = this;
        }

        private void OnMouseExit()
        {
            Deselect();
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

        public void Kill()
        {
            HighlightManager.Instance.OnMonoBehaviourSelected -= DeselectIfNotSelected;
            Destroy(gameObject);
        }
    }
}