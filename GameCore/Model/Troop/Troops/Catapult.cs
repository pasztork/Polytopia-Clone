namespace Model
{
    public class Catapult : OffensiveLandTroop
    {
        public Catapult(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Catapult"];
            Init(player);
        }

        public override List<TileBase> Attack(TroopBase troop)
        {
            IList<TileBase> tilesInRange = GetTilesInAttackRange(TroopProperty.AttackRange);

            if (!tilesInRange.Contains(troop.Tile) || attackedInTurn)
                return null;

            attackedInTurn = true;
            List<TileBase> tilesOfAttackedTroops = new();
            bool damageTaken = troop.TakeDamage(TroopProperty.Damage);

            // igen, fontos, hogy 2x kerüljön bele ha teljesül a feltétel
            if (damageTaken)
                tilesOfAttackedTroops.Add(troop.Tile);
            else
                troop.Player.RaiseOnAttackMissed(this, troop);

            tilesOfAttackedTroops.Add(troop.Tile);

            if (troop.Tile.BuildingOnTop != null && troop.Tile.BuildingOnTop.Player != Player)
                troop.Tile.BuildingOnTop.TakeDamage(TroopProperty.Damage);

            tilesOfAttackedTroops.AddRange(AttackNeighbors(troop.Tile));

            return (tilesOfAttackedTroops.Count == 1) ? null : tilesOfAttackedTroops;
        }

        public override List<TileBase> Attack(BuildingBase building)
        {
            IList<TileBase> tilesInRange = GetTilesInAttackRange(TroopProperty.AttackRange);

            if (!tilesInRange.Contains(building.Tile) || attackedInTurn)
                return null;

            attackedInTurn = true;
            List<TileBase> attackedTiles = new();
            building.TakeDamage(TroopProperty.Damage);
            attackedTiles.Add(building.Tile);

            var target = building.Tile.TroopOnTop;
            if (target != null && target.Player != Player)
            {
                bool damageTaken = target.TakeDamage(TroopProperty.Damage);
                if (damageTaken)
                    attackedTiles.Add(building.Tile);
                else
                    target.Player.RaiseOnAttackMissed(this, target);

            }
            attackedTiles.AddRange(AttackNeighbors(building.Tile));

            return attackedTiles;
        }

        private List<TileBase> AttackNeighbors(TileBase tile)
        {
            List<TileBase> attackedNeighbors = new List<TileBase>();
            foreach (TileBase neighbor in tile.Neighbors)
            {
                var targetTroop = neighbor.TroopOnTop;
                if (targetTroop != null && targetTroop.Player != Player)
                {
                    bool damageTaken = targetTroop.TakeDamage(TroopProperty.Damage / 2);
                    if (damageTaken)
                        attackedNeighbors.Add(neighbor);
                    else
                        targetTroop.Player.RaiseOnAttackMissed(this, targetTroop);
                }

                if (neighbor.BuildingOnTop != null && neighbor.BuildingOnTop.Player != Player)
                    neighbor.BuildingOnTop.TakeDamage(TroopProperty.Damage / 2);
            }
            return attackedNeighbors;
        }

        private IList<TileBase> GetTilesInAttackRange(int range)
        {
            IList<TileBase> allTiles = base.GetTilesInRange(range);
            IList<TileBase> notReachables = base.GetTilesInRange(range - 1);

            var reachables = allTiles.ToHashSet();

            foreach (TileBase tile in notReachables)
                reachables.Remove(tile);

            reachables.Remove(Tile);
            return reachables.ToList();
        }
        public override string ToString()
        {
            return "Catapult";
        }
    }
}