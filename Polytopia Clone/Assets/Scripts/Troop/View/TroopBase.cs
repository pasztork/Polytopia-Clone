using Controller;
using System.Collections;
using System.Collections.Generic;
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
        public Color TroopColor { get => GetComponent<Renderer>().material.color; set => GetComponent<Renderer>().material.color = value; }

        public abstract Model.TroopBase ToModel(Model.Player player);

        private void Awake()
        {
            startColor = TroopColor;
            HighlightManager.Instance.OnMonoBehaviourSelected += DeselectIfNotSelected;
        }

        protected void DeselectIfNotSelected(MonoBehaviour mono)
        {
            if (mono == this)
                return;

            TroopColor = startColor;
        }

        private void OnMouseEnter()
        {
            if (EventSystem.current.IsPointerOverGameObject() || TroopColor == selectColor)
            {
                return;
            }

            TroopColor = hoverColor;
        }

        private void OnMouseOver()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(TroopColor == selectColor)
                {
                    Controller.TroopManager.Instance.SelectedTroop = this;
                }
                else
                {
                    DeselectAttack();
                    SelectMove();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if(TroopColor == selectColor)
                {
                    Controller.TroopManager.Instance.SelectedTroop = this;
                }
                else
                {
                    DeselectMove();
                    SelectAttack();
                }
            }
        }

        //protected virtual void OnMouseDown()
        //{
        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        Deselect();
        //        return;
        //    }

        //    TroopBase selectedTroop = Controller.TroopManager.Instance.SelectedTroop;
        //    if (selectedTroop != this && selectedTroop != null)
        //    {
        //        Controller.TroopManager.Instance.Attack(this);
        //        return;
        //    }

        //    Controller.MapManager.Instance.SelectedTile = null;
        //    Controller.BuildingManager.Instance.SelectedBuilding = null;
        //    Controller.TroopManager.Instance.SelectedTroop = this;
        //}

        private void OnMouseExit()
        {
            if(Controller.TroopManager.Instance.SelectedTroop != this && TroopColor != selectColor)
            {
                TroopColor = startColor;
            }
        }

        public void Deselect()
        {
            if (Controller.TroopManager.Instance.SelectedTroop != this)
                TroopColor = startColor;
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

        public void SelectMove()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                DeselectMove();
                return;
            }

            var prev = TroopManager.Instance.SelectedTroop;
            TroopManager.Instance.SelectedTroop = this;

            if (prev != null)
            {
                prev.DeselectMove();
            }

            IList<Tile> tiles = Controller.TroopManager.Instance.GetTilesForMove();
            foreach (var tile in tiles)
            {
                tile.GetComponent<Renderer>().material.color = tile.SelectColor;
            }
            TroopColor = hoverColor;
        }

        public void SelectAttack()
        {

        }

        public void DeselectMove()
        {

        }

        public void DeselectAttack()
        {

        }
    }
}