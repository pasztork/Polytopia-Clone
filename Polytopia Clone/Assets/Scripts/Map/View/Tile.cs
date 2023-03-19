using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        Controller.MapManager.Instance.OnTileSelected += (tile) =>
        {
            if (tile == this)
                return;

            GetComponent<Renderer>().material.color = StartColor;
        };
    }

    private void OnMouseEnter()
    {
        if (TroopManager.Instance.SelectedTroop?.Tiles.Contains(this) ?? false)
        {
            GetComponent<Renderer>().material.color = hoverColor;
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Deselect();
            return;
        }

        GetComponent<Renderer>().material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        if (TroopManager.Instance.SelectedTroop?.Tiles.Contains(this) ?? false)
        {
            GetComponent<Renderer>().material.color = selectColor;
            return;
        }
        Deselect();
    }

    private void OnMouseDown() =>
        Select();

    public void Select()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Deselect();
            return;
        }
        if (Controller.MapManager.Instance.SelectedTile == this)
        {
            Controller.MapManager.Instance.SelectedTile = null;
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
