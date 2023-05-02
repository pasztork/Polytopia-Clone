using System.Collections.Generic;
using System.Linq;

namespace View
{
    public abstract class WorkerTroop : TroopBase
    {
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
                    if(View.MapManager.Instance.ViewToModelMap[tile].TroopOnTop == null)
                        reachables.Add(tile);
                }
            }
            reachables.Remove(currentTile);
            return reachables.ToList();
        }
    }
}
