namespace Model
{
    public abstract class TraversableTile : TileBase
    {
        public override bool TrainTroop(TroopBase troop)
        {
            if (TroopOnTop != null)
                return false;

            bool success = troop.Train(this);
            if (!success)
                return false;

            TroopOnTop = troop;
            return true;
        }

        public override bool AcceptTroop(TroopBase troop)
        {
            if (TroopOnTop != null)
                return false;

            bool success = troop.Relocate(this);
            if (!success)
                return false;

            TroopOnTop = troop;
            return true;
        }
    }
}