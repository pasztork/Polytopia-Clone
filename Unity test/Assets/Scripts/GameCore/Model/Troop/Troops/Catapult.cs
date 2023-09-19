using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Catapult : OffensiveLandTroop
    {
        public Catapult(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Catapult"];
            Init(player);
        }

        public Catapult() : base()
        {
            initialValues = TroopProperties["Catapult"];
            Init(GameManager.Get<TurnManagerBase>().CurrentPlayer);
        }

        public override List<TileBase> Attack(TroopBase troop)
        {
            IList<TileBase> tilesInRange = GetTilesInAttackRange(TroopProperty.AttackRange);

            if (!tilesInRange.Contains(troop.Tile) || AttackedInTurn)
                return null;

            AttackedInTurn = true;
            List<TileBase> tilesOfAttackedTroops = new();
            bool damageTaken = troop.TakeDamage(TroopProperty.Damage);

            // igen, fontos, hogy 2x kerüljön bele ha teljesül a feltétel
            if (damageTaken)
                tilesOfAttackedTroops.Add(troop.Tile);
            //else
            //    troop.Player.RaiseOnAttackMissed(this, troop);

            tilesOfAttackedTroops.Add(troop.Tile);

            BuildingBase building = troop.Tile.BuildingOnTop;
            if (building != null && building.Player != Player)
                building.TakeDamage(TroopProperty.Damage);

            tilesOfAttackedTroops.AddRange(AttackNeighbors(troop.Tile));

            if(tilesOfAttackedTroops.Count == 1 &&
                building == null &&
                troop.Tile.Neighbors.Count(n => n.BuildingOnTop != null) == 0)
            {
                Player.RaiseOnAttackMissed(this, troop);
                return null;
            }

            return tilesOfAttackedTroops;
        }

        public override List<TileBase> Attack(BuildingBase building)
        {
            IList<TileBase> tilesInRange = GetTilesInAttackRange(TroopProperty.AttackRange);

            if (!tilesInRange.Contains(building.Tile) || AttackedInTurn)
                return null;

            AttackedInTurn = true;
            List<TileBase> attackedTiles = new();
            building.TakeDamage(TroopProperty.Damage);
            attackedTiles.Add(building.Tile);

            var target = building.Tile.TroopOnTop;
            if (target != null && target.Player != Player)
            {
                bool damageTaken = target.TakeDamage(TroopProperty.Damage);
                if (damageTaken)
                    attackedTiles.Add(building.Tile);
                //else
                //    target.Player.RaiseOnAttackMissed(this, target);

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
                    //else
                    //    targetTroop.Player.RaiseOnAttackMissed(this, targetTroop);
                }

                if (neighbor.BuildingOnTop != null && neighbor.BuildingOnTop.Player != Player)
                    neighbor.BuildingOnTop.TakeDamage(TroopProperty.Damage / 2);
            }
            return attackedNeighbors;
        }

        protected override IList<TileBase> GetTilesInAttackRange(int range)
        {
            IList<TileBase> allTiles = base.GetTilesInMovementRange(range);
            IList<TileBase> notReachables = base.GetTilesInMovementRange(range - 1);


            foreach (TileBase tile in notReachables)
                allTiles.Remove(tile);

            allTiles.Remove(Tile);
            return allTiles;
        }
        public override string ToString()
        {
            return "Catapult";
        }
    }
}