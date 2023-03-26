namespace Model
{
    public abstract class NonTraversableTile : TileBase
    {
        public override bool AcceptTroop(TroopBase troop)
        {
            if (TroopOnTop != null)
                return false;

            if(troop is WaterTroop)
            {
                troop.Tile.TroopOnTop = null;
                TroopOnTop = troop;
                return true;
            }
            return false;
        }

        public override bool TrainTroop(TroopBase troop)
        {
            if (TroopOnTop != null)
                return false;

            if (troop is WaterTroop)
            {
                TroopOnTop = troop;
                return true;
            }
            return false;
        }
    }
}
