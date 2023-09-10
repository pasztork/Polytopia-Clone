namespace Model
{
    public abstract class WaterTroop : TroopBase
    {
        public override bool Relocate(TraversableTile target)
        {
            return false;
        }

        public override bool Relocate(WaterTile target)
        {
            Tile.TroopOnTop = null;
            MsovedInTurn = true;
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
