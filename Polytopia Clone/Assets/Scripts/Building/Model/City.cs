using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class City : TroopTrainingBuilding
    {
        private readonly int range;

        public City(int range) : base()
        {
            this.range = range;
            TurnManager.Instance.OnTurnStarted +=
                (player) => troopTrained = false;
        }

        public override IList<TileBase> GetTilesInRange()
        {
            ISet<TileBase> reachables = new HashSet<TileBase>() { Tile };
            for (int i = 0; i < range; i++)
            {
                ISet<TileBase> toAdd = new HashSet<TileBase>();
                foreach (TileBase reachable in reachables)
                    foreach (TileBase tile in reachable.Neighbors)
                        toAdd.Add(tile);

                foreach (TileBase tile in toAdd)
                    reachables.Add(tile);
            }
            reachables.Remove(Tile);
            return reachables.ToList();
        }

        public override void DestroyEveryThingInRange(ISet<TileBase> availableTiles)
        {
            var cityRange = GetTilesInRange();
            foreach (TileBase tile in cityRange)
            {
                if(!availableTiles.Contains(tile) && tile.BuildingOnTop != null && tile.BuildingOnTop.Player == Player)
                tile.BuildingOnTop.TakeDamage(tile.BuildingOnTop.BuildingProperty.Health);
            }
        }
    }
}