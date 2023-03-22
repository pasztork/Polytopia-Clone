using System.Collections.Generic;

namespace Model
{
    public class Player
    {
        public ResourceContainer ResourceContainer { get; private set; } = new ResourceContainer();
        public Dictionary<string, int> ActionCount { get; set; }
        public IList<BuildingBase> Buildings { get; } = new List<BuildingBase>();
        public IList<TroopBase> Troops { get; } = new List<TroopBase>();
        public string Name { get; set; }

        public IList<string> AvailableBuildings { get; set; }
        public IList<string> AvailableTroops { get; set; }

        public Player(string name, Dictionary<string, int> actionCount)
        {
            Name = name;
            ActionCount = actionCount;
            TurnManager.Instance.PlayerCreated(this);
        }

        public void StartTurn()
        {
            ResourceContainer.StartTurn();
        }

        public void EndTurn()
        {

        }

        public bool Build(TileBase tile, BuildingBase building)
        {
            if (!ResourceContainer.HasEnoughFor(building.Cost))
            {
                building.StopProduction();
                return false;
            }

            bool built = tile.SetBuildingOnTop(building);
            if (!built)
            {
                building.StopProduction();
                return false;
            }

            ResourceContainer -= building.Cost;
            building.Tile = tile;
            Buildings.Add(building);
            return true;
        }

        public bool Train(BuildingBase building, TroopBase troop)
        {
            if (!Buildings.Contains(building) ||
                !ResourceContainer.HasEnoughFor(troop.Cost))
                return false;

            bool trained = building.Tile.TrainTroop(troop);
            if (!trained)
                return false;

            ResourceContainer -= troop.Cost;
            Troops.Add(troop);
            return true;
        }

        public bool MoveTroop(TroopBase troop, TileBase target)
        {
            if (!Troops.Contains(troop))
                return false;

            bool moved = troop.Move(target);
            return moved;
        }

        public bool Attack(TroopBase attacker, TroopBase target)
        {
            if (Troops.Contains(attacker) && Troops.Contains(target) || !Troops.Contains(attacker))
                return false;

            return attacker.Attack(target);
        }
    }
}
