using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class City : BuildingBase
    {
        private readonly int range;

        public City(int range) : base()
        {
            this.range = range;
        }

        public override bool TrainTroop(TroopBase troop)
        {
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