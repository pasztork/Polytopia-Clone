using System.Collections.Generic;

namespace Model
{
    public class Player
    {
        public ResourceContainer ResourceContainer { get; private set; } = new ResourceContainer();
        public Dictionary<string, int> ActionCount { get; set; }
        public IList<BuildingBase> Buildings { get; } = new List<BuildingBase>();
        public string Name { get; set; }

        private IList<string> availableBuildings = new List<string>();
        public IList<string> AvailableBuildings { get => availableBuildings; set => availableBuildings = value; }


        public Player()
        {
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
                return false;

            bool built = tile.SetBuildingOnTop(building);
            if (!built)
                return false;

            ResourceContainer -= building.Cost;
            Buildings.Add(building);
            return true;
        }

        private bool CanBuild(BuildingBase building) =>
            ResourceContainer.HasEnoughFor(building.Cost);
    }
}
