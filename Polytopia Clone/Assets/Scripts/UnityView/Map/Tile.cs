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

        [Header("Colors")]
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
            if (previousColor != hoverColor || previousColor != startColor)
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
                TileColor = startColor;
        }
    }
}