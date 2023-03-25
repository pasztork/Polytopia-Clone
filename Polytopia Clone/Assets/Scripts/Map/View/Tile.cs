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
        [SerializeField] private Color selectColor;
        public Color SelectColor { get => selectColor; set => selectColor = value; }
        public Color TileColor 
        { 
            get => GetComponent<Renderer>().material.color;
            set
            {
                previousColor = TileColor;
                GetComponent<Renderer>().material.color = value;
            }
        }
        private Color previousColor;

        private void Awake()
        {
            startColor = TileColor;

            HighlightManager.Instance.OnMonoBehaviourSelected += (mono) =>
            {
                if (mono == this)
                    return;

                TileColor = startColor;
            };
        }

        private void OnMouseEnter()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            TileColor = hoverColor;
        }

        private void OnMouseExit()
        {
            if(previousColor != hoverColor || previousColor != startColor)
            {
                TileColor = previousColor;
            }
            else
            {
                Deselect();
            }
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            if (Controller.TroopManager.Instance.SelectedTroop != null && previousColor == selectColor)
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
                TileColor = startColor;
        }
    }
}