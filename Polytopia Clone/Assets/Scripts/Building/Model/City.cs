using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class City : BuildingBase
    {
        private readonly int range;
        private bool troopTrained;

        public City(int range) : base()
        {
            this.range = range;
            TurnManager.Instance.OnTurnStarted +=
                (player) => troopTrained = false;
        }

        public override bool TrainTroop(TroopBase troop)
        {
            if (troopTrained)
                return false;

            troopTrained = true;
            return Tile.TrainTroop(troop);
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
    }
}