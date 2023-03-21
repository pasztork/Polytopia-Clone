using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class Tile : MonoBehaviour
    {
        public List<Tile> Neighbors { get; } = new List<Tile>();

        [SerializeField] private Color hoverColor;
        public Color startColor;

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
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            GetComponent<Renderer>().material.color = hoverColor;
        }

        private void OnMouseExit()
        {
            Deselect();
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            if (Controller.TroopManager.Instance.SelectedTroop != null)
            {
                Controller.TroopManager.Instance.MoveSelectedTroop(this);
                return;
            }

            Controller.TroopManager.Instance.SelectedTroop = null;
            Controller.BuildingManager.Instance.SelectedBuilding = null;
            Controller.MapManager.Instance.SelectedTile = this;
        }

        public void Deselect()
        {
            if (Controller.MapManager.Instance.SelectedTile != this)
                GetComponent<Renderer>().material.color = startColor;
        }
    }
}