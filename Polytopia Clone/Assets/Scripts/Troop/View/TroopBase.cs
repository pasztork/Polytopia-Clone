using Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private IList<Tile> TilesToHighLight = new List<Tile>();
        private IList<TroopBase> EnemiesToHighLight = new List<TroopBase>();

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
                    TroopManager.Instance.SelectedTroop = this;
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
                    TroopManager.Instance.SelectedTroop = this;
                }
                else
                {
                    DeselectMove();
                    SelectAttack();
                }
            }
        }

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

        public void Move(Tile from, Tile to)
        {
            Func<Tile, IEnumerable<Tile>> shortestPath = FindPath(from);
            IEnumerable<Tile> path = shortestPath(to);
            StartCoroutine(MoveAlong(path));
        }

        private Func<Tile, IEnumerable<Tile>> FindPath(Tile start)
        {
            Dictionary<Tile, Tile> previous = new Dictionary<Tile, Tile>();
            Queue<Tile> queue = new Queue<Tile>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Tile t = queue.Dequeue();
                foreach (Tile neighbor in t.Neighbors)
                {
                    if (previous.ContainsKey(neighbor) || !TilesToHighLight.Contains(neighbor))
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
                while (!current.Equals(start))
                {
                    path.Add(current);
                    current = previous?[current];
                };

                path.Add(start);
                path.Reverse();

                return path;
            };
            return shortestPath;
        }

        private IEnumerator MoveAlong(IEnumerable<Tile> path)
        {
            MapManager.Instance.SelectedTile = null;
            Vector3 offset = new Vector3(1f, 1.5f, 1f);
            foreach (Tile step in path)
            {
                Vector3 center = step.gameObject.transform.position;
                Vector3 nextPosition = center + offset;
                gameObject.transform.position = nextPosition;
                yield return new WaitForSeconds(0.25f);
            }
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

            TilesToHighLight.Clear();
            TilesToHighLight = GetTilesInRange(troopProperties.MovementRange);
            foreach (var tile in TilesToHighLight)
            {
                tile.GetComponent<Renderer>().material.color = tile.SelectColor;
            }
            TroopColor = hoverColor;
        }

        public void SelectAttack()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                DeselectAttack();
                return;
            }

            var prev = TroopManager.Instance.SelectedTroop;
            TroopManager.Instance.SelectedTroop = this;

            if (prev != null)
            {
                prev.DeselectAttack();
            }

            EnemiesToHighLight.Clear();
            EnemiesToHighLight = GetEnemiesInRange(troopProperties.AttackRange);
            foreach(TroopBase enemy in EnemiesToHighLight)
            {
                enemy.TroopColor = selectColor;
            }
            TroopColor = hoverColor;
        }

        public void DeselectMove()
        {
            foreach (Tile tile in TilesToHighLight)
            {
                tile.TileColor = tile.startColor;
            }
            TroopColor = startColor;
            TilesToHighLight.Clear();
        }

        public void DeselectAttack()
        {
            foreach(TroopBase enemy in EnemiesToHighLight)
            {
                enemy.TroopColor = startColor;
            }
            EnemiesToHighLight.Clear();
        }

        private IList<Tile> GetTilesInRange(int range)
        {
            Model.TroopBase modelTroop = TroopManager.Instance.ViewToModelMap[this];
            Tile currentTile = MapManager.Instance.ModelToViewMap[modelTroop.Tile];
            ISet<Tile> reachables = new HashSet<Tile> { currentTile };
            for (int i = 0; i < range; i++)
            {
                ISet<Tile> toAdd = new HashSet<Tile>();
                foreach (Tile reachable in reachables)
                    foreach (Tile tile in reachable.Neighbors)
                        toAdd.Add(tile);

                foreach (Tile tile in toAdd)
                {
                    if(!tile.CompareTag("Water"))
                        reachables.Add(tile);
                }
            }
            reachables.Remove(currentTile);
            return reachables.ToList();
        }

        private IList<TroopBase> GetEnemiesInRange(int range)
        {
            Model.TroopBase modelTroop = TroopManager.Instance.ViewToModelMap[this];
            IList<Model.TileBase> tiles = modelTroop.TilesInAttackRange;
            IList<TroopBase> enemies = new List<TroopBase>();
            foreach(Model.TileBase tile in tiles)
            {
                if(tile.TroopOnTop != null && tile.TroopOnTop.Player != modelTroop.Player)
                {
                    enemies.Add(TroopManager.Instance.ModelToViewMap[tile.TroopOnTop]);
                }
            }
            return enemies.ToList();
        }
    }
}