using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color neighborColor;

    public List<GameObject> Neighbors { get; } = new List<GameObject>();
    public Color StartColor { get; private set; }

    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        StartColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        rend.material.color = highlightColor;
        HighlightNeighbors();
    }

    private void OnMouseExit()
    {
        rend.material.color = StartColor;
        UnhighlightNeighbors();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (BuildManager.selectedTile == this)
        {
            BuildCanvas.Instance.Hide();
            BuildManager.selectedTile = null;
            return;
        }

        BuildCanvas.Instance.MoveToTile(this);
        BuildManager.selectedTile = this;
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
            neighbor.GetComponent<Renderer>().material.color = neighbor.GetComponent<Tile>().StartColor;
        }
    }
}
