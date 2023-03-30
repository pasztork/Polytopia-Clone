using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        public Vector3 Offset { get => offset; private set => offset = value; }
        public List<Tile> Neighbors { get; } = new List<Tile>();

        public Color StartColor { get; private set; }
        private Color hoverColor = Color.yellow;
        private Color selectColor = Color.gray;
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
            StartColor = TileColor;

            HighlightManager.Instance.OnMonoBehaviourSelected += (mono) =>
            {
                if (mono == this)
                    return;

                TileColor = StartColor;
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
            if (previousColor != hoverColor || previousColor != StartColor)
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

            if (View.TroopManager.Instance.SelectedTroop != null && previousColor == selectColor)
            {
                View.TroopManager.Instance.MoveSelectedTroop(this);
                return;
            }

            View.TroopManager.Instance.SelectedTroop = null;
            View.BuildingManager.Instance.SelectedBuilding = null;
            View.MapManager.Instance.SelectedTile = this;
        }

        public void Deselect()
        {
            if (View.MapManager.Instance.SelectedTile != this)
                TileColor = StartColor;
        }
    }
}