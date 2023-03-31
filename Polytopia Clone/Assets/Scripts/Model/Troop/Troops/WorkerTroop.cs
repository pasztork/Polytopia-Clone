namespace Model
{
    public abstract class WorkerTroop : TroopBase
    {
        public override bool Relocate(TraversableTile target)
        {
            return MoveTo(target);
        }

        public override bool Relocate(WaterTile target)
        {
            return MoveTo(target);
        }

        public override bool Relocate(RockTile target)
        {
            return MoveTo(target);
        }

        protected bool MoveTo(TileBase tile)
        {
            Tile.TroopOnTop = null;
            movedInTurn = true;
            Tile = tile;
            return true;
        }

        public override bool Train(TraversableTile tile)
        {
            Tile = tile;
            return true;
        }

        public override bool Train(NonTraversableTile tile)
        {
            return false;
        }

        public override bool Train(WaterTile tile)
        {
            return false;
        }
    }
}