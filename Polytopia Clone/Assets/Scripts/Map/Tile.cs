using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public List<Tile> Neighbors { get; } = new List<Tile>();

    [SerializeField] private Color neighborColor;

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        HighlightNeighbors();
    }

    private void OnMouseExit()
    {
        UnhighlightNeighbors();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (MapManager.Instance.SelectedTile == this)
        {
            MapManager.Instance.SelectedTile = null;
            return;
        }

        MapManager.Instance.SelectedTile = this;

    }

    private void HighlightNeighbors()
    {
        foreach (Tile neighbor in Neighbors)
        {
            if (MapManager.Instance.SelectedTile?.gameObject != neighbor.gameObject)
            {
                neighbor.GetComponent<Renderer>().material.color = neighbor.GetComponent<Renderer>().material.color + neighborColor;
            }
        }
    }

    private void UnhighlightNeighbors()
    {
        foreach (Tile neighbor in Neighbors)
        {
            if (MapManager.Instance.SelectedTile?.gameObject != neighbor.gameObject)
            {
                neighbor.GetComponent<Renderer>().material.color = neighbor.GetComponent<HoverEffect>().StartColor;
            }
        }
    }
}
