using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class City : TroopTrainingBuilding
    {
        public City(Player player) : base()
        {
            initialValues = BuildingProperties["City"];
            Init(player);
        }

        public override IList<TileBase> GetTilesInRange()
        {
            ISet<TileBase> reachables = new HashSet<TileBase>() { Tile };
            for (int i = 0; i < BuildingProperty.Range; i++)
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
                if (!availableTiles.Contains(tile) && tile.BuildingOnTop != null && tile.BuildingOnTop.Player == Player)
                    tile.BuildingOnTop.TakeDamage(tile.BuildingOnTop.BuildingProperty.Health);
            }
        }

        public override string ToString()
        {
            return "City";
        }
    }
}