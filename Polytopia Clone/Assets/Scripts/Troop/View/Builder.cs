using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class Builder : WorkerTroop
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
