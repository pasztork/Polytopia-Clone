using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        private Color hoverColor = Color.yellow;
        private Color selectColor = Color.magenta;
        private Color startColor;


        private IList<Tile> TilesToHighLight = new List<Tile>();
        private IList<TroopBase> EnemiesToHighLight = new List<TroopBase>();

        public abstract Model.TroopBase ToModel(Model.Player player);

        private void Awake()
        {
            startColor = GetComponent<Renderer>().material.color;
            HighlightManager.Instance.OnMonoBehaviourSelected += DeselectIfNotSelected;
        }

        private void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().Initialize(troopProperties.Health);
        }

        protected void DeselectIfNotSelected(MonoBehaviour mono)
        {
            if (mono == this)
                return;

            GetComponent<Renderer>().material.color = startColor;
        }

        private void OnMouseEnter()
        {
            if (EventSystem.current.IsPointerOverGameObject() || GetComponent<Renderer>().material.color == selectColor
                || !Model.TurnManager.Instance.CurrentPlayer.Troops.Contains(Controller.TroopManager.Instance.ViewToModelMap[this]))
            {
                return;
            }

            GetComponent<Renderer>().material.color = hoverColor;
        }

        private void OnMouseOver()
        {
            if (!Model.TurnManager.Instance.CurrentPlayer.Troops.Contains(Controller.TroopManager.Instance.ViewToModelMap[this]))
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                DeselectAttack();
                SelectMove();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                DeselectMove();
                SelectAttack();
            }
        }

        private void OnMouseDown()
        {
            if (GetComponent<Renderer>().material.color == selectColor)
            {
                Controller.TroopManager.Instance.Attack(this);
            }
        }

        private void OnMouseExit()
        {
            if (!Model.TurnManager.Instance.CurrentPlayer.Troops.Contains(Controller.TroopManager.Instance.ViewToModelMap[this]))
            {
                return;
            }

            if (Controller.TroopManager.Instance.SelectedTroop != this && GetComponent<Renderer>().material.color != selectColor)
            {
                GetComponent<Renderer>().material.color = startColor;
            }
        }

        public void Deselect()
        {
            if (Controller.TroopManager.Instance.SelectedTroop != this)
                GetComponent<Renderer>().material.color = startColor;
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

            Func<Tile, IEnumerable<Tile>> shortestPath = v =>
            {
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
            Controller.MapManager.Instance.SelectedTile = null;
            Vector3 offset = new Vector3(1f, 1.5f, 1f);
            foreach (Tile step in path)
            {
                Vector3 center = step.gameObject.transform.position;
                Vector3 nextPosition = center + offset;
                gameObject.transform.position = nextPosition;
                yield return new WaitForSeconds(0.25f);
            }
        }

        public void SelectMove()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                DeselectMove();
                return;
            }

            var prev = Controller.TroopManager.Instance.SelectedTroop;
            Controller.TroopManager.Instance.SelectedTroop = this;

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
            GetComponent<Renderer>().material.color = hoverColor;
        }

        public void SelectAttack()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                DeselectAttack();
                return;
            }

            var prev = Controller.TroopManager.Instance.SelectedTroop;
            Controller.TroopManager.Instance.SelectedTroop = this;

            if (prev != null)
            {
                prev.DeselectAttack();
            }

            EnemiesToHighLight.Clear();
            EnemiesToHighLight = GetEnemiesInRange(troopProperties.AttackRange);
            foreach (TroopBase enemy in EnemiesToHighLight)
            {
                enemy.GetComponent<Renderer>().material.color = selectColor;
            }
            GetComponent<Renderer>().material.color = hoverColor;
        }

        public void DeselectMove()
        {
            foreach (Tile tile in TilesToHighLight)
            {
                tile.TileColor = tile.startColor;
            }
            GetComponent<Renderer>().material.color = startColor;
            TilesToHighLight.Clear();
        }

        public void DeselectAttack()
        {
            foreach (TroopBase enemy in EnemiesToHighLight)
            {
                if (enemy != null)
                    enemy.GetComponent<Renderer>().material.color = startColor;
            }
            EnemiesToHighLight.Clear();
        }

        private IList<Tile> GetTilesInRange(int range)
        {
            Model.TroopBase modelTroop = Controller.TroopManager.Instance.ViewToModelMap[this];
            Tile currentTile = Controller.MapManager.Instance.ModelToViewMap[modelTroop.Tile];
            ISet<Tile> reachables = new HashSet<Tile> { currentTile };
            for (int i = 0; i < range; i++)
            {
                ISet<Tile> toAdd = new HashSet<Tile>();
                foreach (Tile reachable in reachables)
                    foreach (Tile tile in reachable.Neighbors)
                        toAdd.Add(tile);

                foreach (Tile tile in toAdd)
                {
                    if (!tile.CompareTag("Water") && !tile.CompareTag("Mountain") && Controller.MapManager.Instance.ViewToModelMap[tile].TroopOnTop == null)
                        reachables.Add(tile);
                }
            }
            reachables.Remove(currentTile);
            return reachables.ToList();
        }

        private IList<TroopBase> GetEnemiesInRange(int range)
        {
            Model.TroopBase modelTroop = Controller.TroopManager.Instance.ViewToModelMap[this];
            IList<Model.TileBase> tiles = modelTroop.TilesInAttackRange;
            IList<TroopBase> enemies = new List<TroopBase>();
            foreach (Model.TileBase tile in tiles)
            {
                if (tile.TroopOnTop != null && tile.TroopOnTop.Player != modelTroop.Player)
                {
                    enemies.Add(Controller.TroopManager.Instance.ModelToViewMap[tile.TroopOnTop]);
                }
            }
            return enemies.ToList();
        }

        public void TakeDamage(int remainingHealth)
        {
            if (remainingHealth <= 0)
            {
                HighlightManager.Instance.OnMonoBehaviourSelected -= DeselectIfNotSelected;
                Destroy(gameObject);
                return;
            }

            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().Value = remainingHealth;
        }
    }
}