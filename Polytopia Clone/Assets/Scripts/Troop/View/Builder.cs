using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class Builder : TroopBase
    {
        public ISet<Model.TileBase> TilesToBuild = new HashSet<Model.TileBase>();

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase builder = new Model.Builder();
            builder.OnDamageTaken += TakeDamage;
            builder.TroopProperty = new Model.TroopProperty(
                troopProperties.Health, troopProperties.Damage,
                troopProperties.MovementRange, troopProperties.AttackRange);
            builder.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return builder;
        }

        public override void OnMouseOver()
        {
            if (!Model.TurnManager.Instance.CurrentPlayer.Troops.Contains(Controller.TroopManager.Instance.ViewToModelMap[this]))
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                DeselectBuild();
                SelectMove();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                DeselectMove();
                SelectBuild();
            }
        }

        public void SelectBuild()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                DeselectBuild();
                return;
            }

            Controller.TroopManager.Instance.SelectedTroop = this;

            TilesToBuild = Model.TurnManager.Instance.CurrentPlayer.AvailableTiles;
            var playerColor = Controller.TurnManager.Instance.PlayerColors[Controller.TroopManager.Instance.ViewToModelMap[this].Player.Name]; 
            foreach (var tile in TilesToBuild)
            {
                if(tile.BuildingOnTop == null)
                {
                    Controller.MapManager.Instance.ModelToViewMap[tile].GetComponent<Renderer>().material.color = playerColor;
                }
            }
            GetComponent<Renderer>().material.color = hoverColor;
        }

        public void DeselectBuild()
        {
            foreach (var tile in TilesToBuild)
            {
                if (tile.BuildingOnTop == null)
                {
                    Controller.MapManager.Instance.ModelToViewMap[tile].GetComponent<Renderer>().material.color 
                        = Controller.MapManager.Instance.ModelToViewMap[tile].startColor;
                }
            }
        }

        protected override IList<Tile> GetTilesInRange(int range)
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
                    reachables.Add(tile);
            }
            reachables.Remove(currentTile);
            return reachables.ToList();
        }

        public override void TakeDamage(int remainingHealth)
        {
            if (remainingHealth <= 0)
            {
                HighlightManager.Instance.OnMonoBehaviourSelected -= DeselectIfNotSelected;
                DeselectBuild();
                Destroy(gameObject);
                return;
            }

            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().Value = remainingHealth;
        }
    }
}
