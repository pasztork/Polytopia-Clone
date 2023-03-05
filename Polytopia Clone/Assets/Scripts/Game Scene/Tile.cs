using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    [SerializeField] Color highlightColor;
    [SerializeField] Color neighborColor;

    public List<GameObject> Neighbors { get; } = new List<GameObject>();
    public Color StartColor { get; private set; }

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        StartColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        rend.material.color = highlightColor;
        HighlightNeighbors();
    }

    void OnMouseExit()
    {
        rend.material.color = StartColor;
        UnhighlightNeighbors();
    }

    void OnMouseDown()
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

    void HighlightNeighbors()
    {
        foreach (GameObject neighbor in Neighbors)
        {
            neighbor.GetComponent<Renderer>().material.color = neighbor.GetComponent<Renderer>().material.color + neighborColor;
        }
    }

    void UnhighlightNeighbors()
    {
        foreach (GameObject neighbor in Neighbors)
        {
            neighbor.GetComponent<Renderer>().material.color = neighbor.GetComponent<Tile>().StartColor;
        }
    }
}
