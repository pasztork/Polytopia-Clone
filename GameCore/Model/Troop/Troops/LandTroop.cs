namespace Model
{
    public abstract class LandTroop : TroopBase
    {
        public override bool Relocate(TraversableTile target)
        {
            Tile.TroopOnTop = null;
            MovedInTurn = true;
            Tile = target;
            return true;
        }

        public override bool Relocate(WaterTile target)
        {
            return false;
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
