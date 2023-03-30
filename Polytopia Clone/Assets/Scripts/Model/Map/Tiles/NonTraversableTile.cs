namespace Model
{
    public abstract class NonTraversableTile : TileBase
    {
        public override bool TrainTroop(TroopBase troop)
        {
            return false;
        }
    }
}
