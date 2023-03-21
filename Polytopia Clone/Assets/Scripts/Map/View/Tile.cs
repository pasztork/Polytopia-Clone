using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class Tile : MonoBehaviour
    {
        public List<Tile> Neighbors { get; } = new List<Tile>();

        public Color StartColor;
        [SerializeField] private Color hoverColor;
        [SerializeField] private Color neighborColor;
        [SerializeField] private Color selectColor;
        public Color SelectColor { get => selectColor; private set => selectColor = value; }

        private void Awake()
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

        private void OnMouseExit()
        {
            Deselect();
        }

        private void OnMouseDown()
        {
            if (Controller.TroopManager.Instance.SelectedTroop != null)
            {
                Controller.TroopManager.Instance.MoveSelectedTroop(this);
                return;
            }
            Select();
        }

        public void Select()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Deselect();
                return;
            }

            Controller.MapManager.Instance.SelectedTile = this;
        }

        public void Deselect()
        {
            if (Controller.MapManager.Instance.SelectedTile != this)
                GetComponent<Renderer>().material.color = StartColor;
        }
    }
}