using JsonLog;
using LogView.LogTransformer;
using Model;
using System.Text.Json;
using Util;
using ViewUtil;

namespace Network;

public class NetworkCommandProcessor : JsonCommandProcessorBase
{
    private static NetworkCommandProcessor? instance;

    public static NetworkCommandProcessor Instance
    {
		get
		{
			if (instance == null)
			{
				instance = new NetworkCommandProcessor();
			}
			return instance;
		}
    }

    private readonly CoordinateToModelMapper _coordinateMapper = new();

	private readonly BuildingFactory _buildingFactory = new();

	private readonly TechTreeItemFactory _techTreeItemFactory = new();

	private readonly TroopFactory _troopFactory = new();

	private readonly int[] _startCoords = new int[2];

	private readonly int[] _endCoords = new int[2];

	private NetworkCommandProcessor()
	{
		actions.Add("AttackBuilding", AttackBuilding);
		actions.Add("AttackTroop", AttackTroop);
		actions.Add("Build", Build);
		actions.Add("EndTurn", EndTurn);
		actions.Add("Learn", Learn);
		actions.Add("Move", Move);
		actions.Add("Train", Train);
	}

	public override string GetAvailableActions(JsonActionObject jsonCommand)
	{

		Player player = Model.GameManager.Players.First(p => p.Name.Equals(jsonCommand.Name));
		if(player == null)
		{
			return String.Empty;
		}

		return JsonSerializer.Serialize(new ActionState
		{
			Troops = GetTroops(player),
			Buildings = GetBuildings(player),
			TechsToUnlock = GetTechs(player)
		});
	}

    private List<TroopState> GetTroops(Player player)
	{
		List<TroopState> troops = new();
        foreach (TroopBase troop in player.Troops)
        {
			var tilesToMove = GetTilesToMove(troop);
			var enemiesToAttack = GetEnemies(troop);
			var buildingsToBuild = GetBuildableBuildings(troop);
			if(tilesToMove.Count > 0 || 
				enemiesToAttack.Item1.Count > 0 || 
				enemiesToAttack.Item2.Count > 0 || 
				buildingsToBuild.Count > 0)
			{
                troops.Add(new TroopState
                {
                    Type = troop.ToString()!,
                    Health = troop.TroopProperty.Health,
                    Damage = troop.TroopProperty.Damage,
                    Position = new[]
                {
                    _coordinateMapper.GetCoordinatesOf(troop.Tile).Item1,
                    _coordinateMapper.GetCoordinatesOf(troop.Tile).Item2
                },
                    TilesToMove = tilesToMove,
                    TroopsToAttack = enemiesToAttack.Item1,
					BuildingsToAttack = enemiesToAttack.Item2,
                    BuildingsToBuild = buildingsToBuild
                });
            }
        }
		return troops;
    }

	private List<int[]> GetTilesToMove(TroopBase troop)
	{
		if (troop.MovedInTurn)
		{
			return new();
		}
		List<int[]> tilesToMove = new();
        troop.TilesInMovementRange.Where(t => t.TroopOnTop == null).ToList().ForEach(tile => {
			var coordinates = _coordinateMapper.GetCoordinatesOf(tile);
            tilesToMove.Add(new int[]
			{
				coordinates.Item1,
				coordinates.Item2
			});
		});
        return tilesToMove;
    }

	private (List<EnemyState>, List<EnemyState>) GetEnemies(TroopBase troop)
	{
		if(troop.TroopProperty.AttackRange == 0 || troop.AttackedInTurn)
		{
			return (new(), new());
		}

        List<EnemyState> enemyTroops = new();
        List<EnemyState> enemyBuildings = new();
        foreach (TileBase tile in troop.TilesInAttackRange)
        {
            if (tile.TroopOnTop is not null && troop.Player != tile.TroopOnTop.Player)
            {
                var enemy = tile.TroopOnTop;
                enemyTroops.Add(new EnemyState
                {
                    Name = enemy.Player.Name,
                    Type = enemy.ToString()!,
                    Health = enemy.TroopProperty.Health,
                    Damage = enemy.TroopProperty.Damage,
                    Position = new[] 
					{
                        _coordinateMapper.GetCoordinatesOf(tile).Item1,
                        _coordinateMapper.GetCoordinatesOf(tile).Item2
                    }
                });
            }
			if(tile.BuildingOnTop is not null && troop.Player != tile.BuildingOnTop.Player)
			{
				var enemy = tile.BuildingOnTop;
                enemyBuildings.Add(new EnemyState
				{
					Name = enemy.Player.Name,
					Type = enemy.ToString()!,
					Health = enemy.BuildingProperty.Health,
					Damage = 0,
					Position = new[]
					{
                        _coordinateMapper.GetCoordinatesOf(tile).Item1,
                        _coordinateMapper.GetCoordinatesOf(tile).Item2
                    }
				});
			}
        }
		return (enemyTroops, enemyBuildings);
    }

	private List<BuildableBuildingState> GetBuildableBuildings(TroopBase troop)
	{
        if (troop.Tile.BuildingOnTop is not null)
        {
            return new();
        }

		HashSet<TileBase> tilesInCityRange = troop.Player.AvailableTiles.ToHashSet();
		HashSet<TileBase> occupiedTiles = new HashSet<TileBase>();
		foreach(var player in GameManager.Players)
		{
			occupiedTiles.UnionWith(player.AvailableTiles);
		}

        List<BuildableBuildingState> buildableBuildings = new();
        foreach (var buildingName in troop.Player.AvailableBuildings)
		{
			var building = _buildingFactory.Instantiate(buildingName);
			building.StopProduction();
			troop.Player.Buildings.Remove(building);
			troop.FillRequirements(building.Requirements);
            bool requirementsMet = building.Requirements.RequirementsMet(troop.Player.ResourceContainer, troop.Player.BonusProperty.BuildingDiscount, occupiedTiles, tilesInCityRange, troop.Tile);
            
			if (requirementsMet && 
				troop.Tile.CheckTechRequirement(building, troop.Player) && 
				troop.Player.AvailableBuildings.Contains(buildingName))
			{
				Cost cost = building.Cost * (1f - troop.Player.BonusProperty.BuildingDiscount);
                buildableBuildings.Add(new BuildableBuildingState{ 
					Type =  buildingName, 
					Cost = new CostState
					{
						FoodCost = cost.FoodCost,
						MaterialCost = cost.MaterialCost,
						MoneyCost = cost.MoneyCost
					} 
				});
			}
		}
		return buildableBuildings;
	}

	private List<BuildingState> GetBuildings(Player player)
	{
		List<BuildingState> buildings = new();
		foreach(BuildingBase building in player.Buildings)
		{
			var troopsToTrain = GetTroopsToTrain(building);
			if(troopsToTrain.Count > 0)
			{
                buildings.Add(new BuildingState
                {
                    Type = building.ToString()!,
                    Health = building.BuildingProperty.Health,
                    Position = new[]
                {
                    _coordinateMapper.GetCoordinatesOf(building.Tile).Item1,
                    _coordinateMapper.GetCoordinatesOf(building.Tile).Item2
                },
                    TroopsToTrain = troopsToTrain
                });
            }
		}
		return buildings;
	}

    private List<TrainableTroopState> GetTroopsToTrain(BuildingBase building)
    {
		if(building.Tile.TroopOnTop is not null || building.TroopTrainedInTurn)
		{
			return new();
		}

		List<TrainableTroopState> trainableTroops = new();
		foreach(var troopName in building.Player.AvailableTroops)
		{
            Cost cost = Cost.CreateNewFromJsonCost(TroopBase.TroopProperties[troopName].Cost);
            if (building.TrainableTroops.Contains(troopName) && 
				building.Player.AvailableTroops.Contains(troopName) && 
				building.Player.ResourceContainer.HasEnoughFor(cost))
			{
                trainableTroops.Add(new TrainableTroopState 
				{
					Type = troopName,
					Cost = new CostState
                    {
                        FoodCost = cost.FoodCost,
                        MaterialCost = cost.MaterialCost,
                        MoneyCost = cost.MoneyCost
                    }
                });
			}
		}
		return trainableTroops;
    }

    private List<TechState> GetTechs(Player player)
    {
		List<TechState> availableTechs = new();
        foreach (var tech in player.Techs.Values)
        {
			if (tech.IsAvailable && 
				!tech.TechTreeItemProperty.IsUnlocked && 
				player.ResourceContainer.HasEnoughFor(tech.TechTreeItemProperty.Cost))
			{
				availableTechs.Add(new TechState
				{
					Name = tech.HashCode,
					CostState = new CostState
					{
						FoodCost = tech.TechTreeItemProperty.Cost.FoodCost,
						MaterialCost = tech.TechTreeItemProperty.Cost.MaterialCost,
						MoneyCost = tech.TechTreeItemProperty.Cost.MoneyCost
					}
				});
			}
        }
        return availableTechs;
    }

    private bool AttackBuilding()
	{
		ExtractCoordsFromCommand();

		if (_startCoords.Length < 2 ||
			_endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null ||
			_coordinateMapper.GetBuildingAt(_endCoords[0], _endCoords[1]) is null) { return false; }

		Controller.GameManager.Get<Controller.BuildingManagerBase>().Attack(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_coordinateMapper.GetBuildingAt(_endCoords[0], _endCoords[1]));

		return true;
	}

	private bool AttackTroop()
	{
		ExtractCoordsFromCommand();

		if (_startCoords.Length < 2 ||
			_endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null ||
			_coordinateMapper.GetTroopAt(_endCoords[0], _endCoords[1]) is null) { return false; }

		Controller.GameManager.Get<Controller.TroopManagerBase>().Attack(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_coordinateMapper.GetTroopAt(_endCoords[0], _endCoords[1]));

		return true;
	}

	private bool Build()
	{
		ExtractCoordsFromCommand();
		var buildingName = Command.Parameters.Building;

		if (_startCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null) { return false; }

		bool result = Controller.GameManager.Get<Controller.BuildingManagerBase>().Build(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_buildingFactory.Instantiate(buildingName));

		return result;
	}

	private bool EndTurn()
	{
		Controller.GameManager.Get<Controller.TurnManagerBase>().FinishTurn();
		return true;
	}

	private bool Learn()
	{
		string techName = Command.Parameters.Tech;

		var currentPlayer = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer;
		if (!currentPlayer.Techs.ContainsKey(techName)) { return false; }

		Model.TechTreeItemBase techItem = _techTreeItemFactory.Instantiate(techName);
		bool result = Controller.GameManager.Get<Controller.TechTreeManagerBase>().UnlockTech(techItem);

		return result;
	}

	private bool Move()
	{
		ExtractCoordsFromCommand();

		if (_startCoords.Length < 2 ||
			_endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null ||
			_coordinateMapper.GetTileAt(_endCoords[0], _endCoords[1]) is null) { return false; }

		bool result = Controller.GameManager.Get<Controller.TroopManagerBase>().MoveTroop(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_coordinateMapper.GetTileAt(_endCoords[0], _endCoords[1]));

		return result;
	}

	private bool Train()
	{
		ExtractCoordsFromCommand();
		string troopName = Command.Parameters.Troop;

		if (_startCoords.Length < 2 ||
			_coordinateMapper.GetBuildingAt(_startCoords[0], _startCoords[1]) is null) { return false; }

        bool result = Controller.GameManager.Get<Controller.TroopManagerBase>().Train(
			_coordinateMapper.GetBuildingAt(_startCoords[0], _startCoords[1]),
			_troopFactory.Instantiate(troopName));

		return result;
	}

	private void ExtractCoordsFromCommand()
	{
		ExtractCoordsFromTo(Command.Parameters.Start, _startCoords);
		ExtractCoordsFromTo(Command.Parameters.End, _endCoords);
	}

	private static void ExtractCoordsFromTo(int[] from, int[] to)
	{
		if (from.Length < 2) { return; }

		to[0] = from[0];
		to[1] = from[1];
	}
}
