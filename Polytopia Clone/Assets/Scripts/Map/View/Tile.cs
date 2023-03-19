using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public List<Tile> Neighbors { get; } = new List<Tile>();

    public Color StartColor;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color neighborColor;

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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Deselect();
            return;
        }

        GetComponent<Renderer>().material.color = hoverColor;
        HighlightNeighbors();
    }

    private void OnMouseExit() =>
        Deselect();

    private void OnMouseDown() =>
        Select();

    private void HighlightNeighbors()
    {
        foreach (Tile neighbor in Neighbors)
            if (Controller.MapManager.Instance.SelectedTile?.gameObject != neighbor.gameObject)
                neighbor.GetComponent<Renderer>().material.color = neighbor.GetComponent<Renderer>().material.color + neighborColor;
    }

    private void UnhighlightNeighbors()
    {
        foreach (Tile neighbor in Neighbors)
            if (Controller.MapManager.Instance.SelectedTile?.gameObject != neighbor.gameObject)
                neighbor.GetComponent<Renderer>().material.color = neighbor.GetComponent<Tile>().StartColor;
    }

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
        UnhighlightNeighbors();

        if (Controller.MapManager.Instance.SelectedTile != this)
            GetComponent<Renderer>().material.color = StartColor;
    }
}
