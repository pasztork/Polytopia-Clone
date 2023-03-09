using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public List<GameObject> Neighbors { get; } = new List<GameObject>();

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

        if (BuildManager.Instance.SelectedTile == this)
        {
            BuildCanvas.Instance.Hide();
            BuildManager.Instance.SelectedTile = null;
            return;
        }

        BuildCanvas.Instance.MoveToTile(this);
        BuildManager.Instance.SelectedTile = this;
    }

    private void HighlightNeighbors()
    {
        foreach (GameObject neighbor in Neighbors)
        {
            neighbor.GetComponent<Renderer>().material.color = neighbor.GetComponent<Renderer>().material.color + neighborColor;
        }
    }

    private void UnhighlightNeighbors()
    {
        foreach (GameObject neighbor in Neighbors)
        {
            neighbor.GetComponent<Renderer>().material.color = neighbor.GetComponent<HoverEffect>().StartColor;
        }
    }
}
