namespace Model
{
    public abstract class OffensiveLandTroop : LandTroop
    {
        public override bool AttackedInTurn { get; protected set; }

        public OffensiveLandTroop() : base()
        {
            GameManager.Get<TurnManagerBase>().OnTurnStarted +=
                (player) => AttackedInTurn = false;
        }

        protected override IList<TileBase> GetTilesInMovementRange(int range)
        {
            ISet<TileBase> reachables = new HashSet<TileBase> { Tile };
            for (int i = 0; i < range; i++)
            {
                ISet<TileBase> toAdd = new HashSet<TileBase>();
                foreach (TileBase reachable in reachables)
                    foreach (TileBase tile in reachable.Neighbors)
                        if (tile is TraversableTile)
                            toAdd.Add(tile);

                foreach (TileBase tile in toAdd)
                    reachables.Add(tile);
            }
            reachables.Remove(Tile);
            return reachables.ToList();
        }

        public override List<TileBase> Attack(TroopBase troop)
        {
            if (!TilesInAttackRange.Contains(troop.Tile) || AttackedInTurn)
                return null;

            AttackedInTurn = true;
            bool damageTaken = troop.TakeDamage(TroopProperty.Damage);
            if (!damageTaken)
            {
                Player.RaiseOnAttackMissed(this, troop);
                return null;
            }

            return new List<TileBase>() { troop.Tile, troop.Tile };
        }

        public override List<TileBase> Attack(BuildingBase building)
        {
            IList<TileBase> tilesInRange = TilesInAttackRange;
            tilesInRange.Add(Tile);
            if (!tilesInRange.Contains(building.Tile) || AttackedInTurn)
                return null;

            AttackedInTurn = true;
            building.TakeDamage(TroopProperty.Damage);
            return new List<TileBase>() { building.Tile };
        }

        public override void OffensiveLandMovementRangeBonus(Player player)
        {
            TroopProperty.MovementRange += player.BonusProperty.OffensiveLandMoveBonus;
        }

        public override void ApplyAllPropertyBonus(Player player)
        {
            base.ApplyAllPropertyBonus(player);
            TroopProperty.MovementRange += player.BonusProperty.OffensiveLandMoveBonus;
            TroopProperty.Damage += player.BonusProperty.OffensiveDamageBonus;
        }
    }
}