namespace Model
{
    public abstract class TroopBase
    {
        public Cost Cost { get; set; }
        public TroopProperty TroopProperty { get; set; }
        public TileBase Tile { get; set; }
    }
}
