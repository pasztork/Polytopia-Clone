using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TroopBase : MonoBehaviour
{
    [Header("Cost Settings")]
    [SerializeField] private Cost cost;
    [SerializeField] private TroopProperty troopProperty;
    public Cost Cost { get => cost; }
    public TroopProperty TroopProperty { get => troopProperty; }

    public Tile Tile { private get; set; }
    public HashSet<Tile> Tiles { get; private set; } = new HashSet<Tile>();


    [SerializeField] private Color hoverColor;
    private Color startColor;

    private void Awake()
    {
        startColor = GetComponent<Renderer>().material.color;

        TroopManager.Instance.OnTroopSelected += (troop) =>
        {
            if (troop == this)
                return;

            GetComponent<Renderer>().material.color = startColor;
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

    private void OnMouseDown() =>
        Select();

    private void OnMouseExit() =>
        Deselect();

    public void Select()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Deselect();
            return;
        }

        if (TroopManager.Instance.SelectedTroop == this)
        {
            TroopManager.Instance.SelectedTroop = null;
            return;
        }

        TroopManager.Instance.SelectedTroop = this;

        Tiles.Clear();
        GetTiles();
        foreach (Tile tile in Tiles)
        {
            tile.GetComponent<Renderer>().material.color = tile.SelectColor;
        }
        Controller.MapManager.Instance.OnTileSelected += Move;
    }

    public void Deselect()
    {
        if (TroopManager.Instance.SelectedTroop != this)
        {
            GetTiles();
            foreach (Tile tile in Tiles)
            {
                tile.GetComponent<Renderer>().material.color = tile.StartColor;
            }
            Controller.MapManager.Instance.OnTileSelected -= Move;
            GetComponent<Renderer>().material.color = startColor;
        }
    }

    private void GetTiles()
    {
        Tiles.Add(Tile);
        for(int i = 0; i < TroopProperty.MoveRange; i++)
        {
            int count = Tiles.Count;
            for(int j = 0; j < count; j++)
            {
                foreach(Tile n in Tiles.ElementAt(j).Neighbors)
                {
                    if (n.gameObject.CompareTag("Grass") || n.gameObject.CompareTag("Sand")){
                        Tiles.Add(n);
                    }
                }
            }
        }
    }

    private void Move(Tile tile)
    {
        if (!Tiles.Contains(tile))
        {
            return;
        }
        Func<Tile, IEnumerable<Tile>> shortestPath = FindPathTo(tile);
        IEnumerable<Tile> path = shortestPath(tile);
        StartCoroutine(SmoothMove(path));
    }

    private Func<Tile, IEnumerable<Tile>> FindPathTo(Tile destination)
    {
        Dictionary<Tile, Tile> previous = new Dictionary<Tile, Tile>();
        Queue<Tile> queue = new Queue<Tile>();
        queue.Enqueue(Tile);

        while (queue.Count > 0) 
        {
            Tile t = queue.Dequeue();
            foreach(Tile neighbor in  t.Neighbors)
            {
                if(previous.ContainsKey(neighbor) || !Tiles.Contains(neighbor))
                {
                    continue;
                }
                previous[neighbor] = t;
                queue.Enqueue(neighbor);
            }
        }

        Func<Tile, IEnumerable<Tile>> shortestPath = v => {
            var path = new List<Tile> { };

            var current = v;
            while (!current.Equals(Tile))
            {
                path.Add(current);
                current = previous?[current];
            };

            path.Add(Tile);
            path.Reverse();

            return path;
        };
        return shortestPath;
    }

    private IEnumerator SmoothMove(IEnumerable<Tile> path)
    {
        foreach (var step in path)
        {
            Vector3 center = step.gameObject.GetComponent<TileHolder>().transform.position;
            float diff = step.gameObject.GetComponent<TileHolder>().gameObject.GetComponent<BoxCollider>().size.x;
            Vector3 offset = new Vector3(diff, 1.5f, diff);
            Vector3 nextPosition = center + offset;
            gameObject.transform.position = nextPosition;
            yield return new WaitForSeconds(0.25f);
        }
        Tile = path.Last();
    }
}
