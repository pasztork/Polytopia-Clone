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
            if (!CanBuild(building))
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

        private bool CanBuild(BuildingBase building) =>
            ResourceContainer.HasEnoughFor(building.Cost);

        public bool Train(BuildingBase building, TroopBase troop)
        {
            if (!CanTrain(troop))
                return false;

            bool trained = building.Tile.TrainTroop(troop);
            if (!trained)
                return false;

            ResourceContainer -= troop.Cost;
            troop.Tile = building.Tile;
            Troops.Add(troop);
            return true;
        }

        private bool CanTrain(TroopBase troop)
        {
            return ResourceContainer.HasEnoughFor(troop.Cost);
        }
    }
}
