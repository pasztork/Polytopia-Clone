using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public abstract class WaterTroop : TroopBase
    {
        protected override IList<TileBase> GetTilesInMovementRange(int range)
        {
            ISet<TileBase> reachables = new HashSet<TileBase> { Tile };
            for (int i = 0; i < range; i++)
            {
                ISet<TileBase> toAdd = new HashSet<TileBase>();
                foreach (TileBase reachable in reachables)
                    foreach (TileBase tile in reachable.Neighbors)
                        if(tile.ToString().Equals(nameof(WaterTile)))
                            toAdd.Add(tile);

                foreach (TileBase tile in toAdd)
                    reachables.Add(tile);
            }
            reachables.Remove(Tile);
            return reachables.ToList();
        }

        public override bool Relocate(TraversableTile target)
        {
            return false;
        }

        public override bool Relocate(WaterTile target)
        {
            Tile.TroopOnTop = null;
            Tile = target;
            return true;
        }

        public override bool Train(TraversableTile tile)
        {
            return false;
        }

        public override bool Train(NonTraversableTile tile)
        {
            return false;
        }

        public override bool Train(WaterTile tile)
        {
            Tile = tile;
            return true;
        }

        public override void WaterMovementRangeBonus(Player player)
        {
            TroopProperty.MovementRange += player.BonusProperty.WaterMoveBonus;
        }

        public override void ApplyAllPropertyBonus(Player player)
        {
            base.ApplyAllPropertyBonus(player);
            TroopProperty.MovementRange += player.BonusProperty.WaterMoveBonus;
        }
    }
}
