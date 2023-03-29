using System.Collections.Generic;
using System.Linq;

namespace View
{
    public class Boat : OffensiveTroop
    {
        public override Model.TroopBase ToModel(Model.Player player)
        {
            Model.TroopBase boat = new Model.Boat();
            boat.OnDamageTaken += TakeDamage;
            boat.TroopProperty = new Model.TroopProperty(
                troopProperties.Health, troopProperties.Damage,
                troopProperties.MovementRange, troopProperties.AttackRange);
            boat.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return boat;
        }

        protected override IList<Tile> GetTilesInRange(int range)
        {
            Model.TroopBase modelTroop = Controller.TroopManager.Instance.ViewToModelMap[this];
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
