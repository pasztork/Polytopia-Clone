using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace View
{
    public abstract class OffensiveTroop : TroopBase
    {
        private IList<TroopBase> EnemiesToHighLight = new List<TroopBase>();

        public override void OnMouseOver()
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

        public void SelectAttack()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                DeselectAttack();
                return;
            }

            var prev = Controller.TroopManager.Instance.SelectedTroop;
            Controller.TroopManager.Instance.SelectedTroop = this;

            if(prev != null )
            {
                (prev as OffensiveTroop)?.DeselectAttack();
            }

            EnemiesToHighLight.Clear();
            EnemiesToHighLight = GetEnemiesInRange(troopProperties.AttackRange);
            foreach (TroopBase enemy in EnemiesToHighLight)
            {
                enemy.GetComponent<Renderer>().material.color = selectColor;
            }
            GetComponent<Renderer>().material.color = hoverColor;
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

        public IList<TroopBase> GetEnemiesInRange(int range)
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
    }
}
