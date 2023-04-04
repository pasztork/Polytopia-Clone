using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace View
{
    public class Boat : OffensiveTroop
    {
        private void Start()
        {
            GetComponentInChildren<Canvas>().GetComponentInChildren<HealthBar>().
                Initialize(Model.TroopBase.TroopProperties.Boat.Health);
            movementRange = Model.TroopBase.TroopProperties.Boat.MovementRange;
        }

        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase boat = new Model.Boat(player);
            boat.OnDamageTaken += TakeDamage;
            boat.OnTroopHealed += Heal;
            return boat;
        }

        protected override IList<Tile> GetTilesInRange(int range)
        {
            Model.TroopBase modelTroop = View.TroopManager.Instance.ViewToModelMap[this];
            Tile currentTile = View.MapManager.Instance.ModelToViewMap[modelTroop.Tile];
            ISet<Tile> reachables = new HashSet<Tile> { currentTile };
            for (int i = 0; i < range; i++)
            {
                ISet<Tile> toAdd = new HashSet<Tile>();
                foreach (Tile reachable in reachables)
                    foreach (Tile tile in reachable.Neighbors)
                        toAdd.Add(tile);

                foreach (Tile tile in toAdd)
                {
                    if (tile.CompareTag("Water") && View.MapManager.Instance.ViewToModelMap[tile].TroopOnTop == null)
                        reachables.Add(tile);
                }
            }
            reachables.Remove(currentTile);
            return reachables.ToList();
        }
    }
}
