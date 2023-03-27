using System;
using System.Collections.Generic;

namespace Model
{
    public class Player
    {
        public event Action<Player, TileBase, BuildingBase> OnStartingCitySpawned;
        public event Action<Player> OnEliminited;

        public ResourceContainer ResourceContainer { get; private set; } = new ResourceContainer();

        public IList<TechTreeItemBase> UnlockedTechs { get; private set; } = new List<TechTreeItemBase>();

        public Dictionary<string, int> StartingProduction { private get; set; }
        public int StartingCityRange { private get; set; }
        public IList<BuildingBase> Buildings { get; } = new List<BuildingBase>();
        public IList<TroopBase> Troops { get; } = new List<TroopBase>();
        public ISet<TileBase> AvailableTiles { get; } = new HashSet<TileBase>();

        public string Name { get; set; }

        public IList<string> AvailableBuildings { get; set; }
        public IList<string> AvailableTroops { get; set; }

        public Player(string name)
        {
            Name = name;
            TurnManager.Instance.PlayerCreated(this);
            GameManager.Instance.Players.Add(this);
        }

        public void StartTurn()
        {
            ResourceContainer.StartTurn();
        }

        public void EndTurn()
        {

        }

        public bool Build(TroopBase troop, BuildingBase building)
        {
            RequirementsListBase requirements = building.Requirements;
            troop.FillRequirements(requirements);
            bool requirementsMet = requirements.RequirementsMet(ResourceContainer, AvailableTiles, troop.Tile);
            if (!requirementsMet || !Troops.Contains(troop))
            {
                building.StopProduction();
                return false;
            }

            bool built = troop.Tile.SetBuildingOnTop(building);
            if (!built)
            {
                building.StopProduction();
                return false;
            }

            building.Tile = troop.Tile;
            ResourceContainer -= building.Cost;
            AddBuilding(building);
            troop.TakeDamage(troop.TroopProperty.Health);
            return true;
        }

        public bool Train(BuildingBase building, TroopBase troop)
        {
            if (!Buildings.Contains(building) ||
                !ResourceContainer.HasEnoughFor(troop.Cost))
                return false;

            bool trained = building.TrainTroop(troop);
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

        public bool Attack(TroopBase attacker, BuildingBase target)
        {
            if (Troops.Contains(attacker) && Buildings.Contains(target) || !Troops.Contains(attacker))
                return false;

            return attacker.Attack(target);
        }

        public void SetupStartingPosition()
        {
            BuildingBase city = new City(StartingCityRange);
            city.Producers.Add(new MoneyProducer(ResourceContainer, StartingProduction["Money"]));
            city.Producers.Add(new MaterialProducer(ResourceContainer, StartingProduction["Material"]));
            city.Producers.Add(new FoodProducer(ResourceContainer, StartingProduction["Food"]));
            TileBase tile = MapManager.Instance.GetStartingTile();
            city.Tile = tile;
            city.Player = this;
            tile.SetBuildingOnTop(city);
            AvailableTiles.Add(tile);
            AddBuilding(city);

            OnStartingCitySpawned?.Invoke(this, tile, city);
        }

        private void AddBuilding(BuildingBase building)
        {
            Buildings.Add(building);
            AvailableTiles.UnionWith(building.GetTilesInRange());
        }

        public void RemoveBuilding(BuildingBase building)
        {
            Buildings.Remove(building);
            GetAllAvailableTiles();
            building.DestroyEveryThingInRange(AvailableTiles);

            if (Buildings.Count == 0)
                OnEliminited.Invoke(this);
        }

        private void GetAllAvailableTiles()
        {
            AvailableTiles.Clear();
            foreach (var building in Buildings)
            {
                AvailableTiles.UnionWith(building.GetTilesInRange());
            }
        }
    }
}
