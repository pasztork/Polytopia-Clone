using JsonLog;
using LogView.LogTransformer;
using Model;
using System.Text.Json;
using Util;
using ViewUtil;

namespace Network;

public class NetworkCommandProcessor : JsonCommandProcessorBase
{
	private readonly CoordinateToModelMapper _coordinateMapper = new();

	private readonly BuildingFactory _buildingFactory = new();

	private readonly TechTreeItemFactory _techTreeItemFactory = new();

	private readonly TroopFactory _troopFactory = new();

	private readonly int[] _startCoords = new int[2];

	private readonly int[] _endCoords = new int[2];

	public NetworkCommandProcessor()
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
			troops.Add(new TroopState
			{
				Type = troop.ToString(),
				Health = troop.TroopProperty.Health,
				Damage = troop.TroopProperty.Damage,
				Position = new[]
				{
                    _coordinateMapper.GetCoordinatesOf(troop.Tile).Item1,
                    _coordinateMapper.GetCoordinatesOf(troop.Tile).Item2
                },
				TilesToMove = GetTilesToMove(troop),
				EnemiesToAttack = GetEnemies(troop),
				BuildingsToBuild = GetBuildableBuildings(troop)
			});
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

	private List<EnemyState> GetEnemies(TroopBase troop)
	{
		if(troop.TroopProperty.AttackRange == 0 || troop.AttackedInTurn)
		{
			return new();
		}

        List<EnemyState> enemies = new();
        foreach (TileBase tile in troop.TilesInAttackRange)
        {
            if (tile.TroopOnTop is not null && troop.Player != tile.TroopOnTop.Player)
            {
                var enemy = tile.TroopOnTop;
                enemies.Add(new EnemyState
                {
                    Name = enemy.Player.Name,
                    Type = enemy.ToString(),
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
				enemies.Add(new EnemyState
				{
					Name = enemy.Player.Name,
					Type = enemy.ToString(),
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
		return enemies;
    }

	private List<BuildableBuildingState> GetBuildableBuildings(TroopBase troop)
	{
        if (troop.Tile.BuildingOnTop is not null)
        {
            return new();
        }

        HashSet<TileBase> tilesInCityRange = new();
		troop.Player.Buildings.ToList().ForEach(building =>
		{
			tilesInCityRange.UnionWith(building.GetTilesInRange());
		});

        List<BuildableBuildingState> buildableBuildings = new();
        foreach (var buildingName in troop.Player.AvailableBuildings)
		{
			var building = _buildingFactory.Instantiate(buildingName);
			troop.FillRequirements(building.Requirements);
            bool requirementsMet = building.Requirements.RequirementsMet(troop.Player.ResourceContainer, troop.Player.BonusProperty.BuildingDiscount, tilesInCityRange, troop.Tile);
            
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
			buildings.Add(new BuildingState
			{
				Type = building.ToString(),
				Health = building.BuildingProperty.Health,
				Position = new[]
				{
                    _coordinateMapper.GetCoordinatesOf(building.Tile).Item1,
                    _coordinateMapper.GetCoordinatesOf(building.Tile).Item2
                },
				TroopsToTrain = GetTroopsToTrain(building)
			});
		}
		return buildings;
	}

    private List<TrainableTroopState> GetTroopsToTrain(BuildingBase building)
    {
		if(building.Tile.TroopOnTop is not null || building.TroopTrained)
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
			if (tech.IsAvailable)
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

    private string AttackBuilding()
	{
		ExtractCoordsFromCommand();

		if (_startCoords.Length < 2 ||
			_endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null ||
			_coordinateMapper.GetBuildingAt(_endCoords[0], _endCoords[1]) is null) { return ERROR; }

		bool result = Controller.GameManager.Get<Controller.BuildingManagerBase>().Attack(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_coordinateMapper.GetBuildingAt(_endCoords[0], _endCoords[1]));

		return result ? OK : ERROR;
	}

	private string AttackTroop()
	{
		ExtractCoordsFromCommand();

		if (_startCoords.Length < 2 ||
			_endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null ||
			_coordinateMapper.GetTroopAt(_endCoords[0], _endCoords[1]) is null) { return ERROR; }

		bool result = Controller.GameManager.Get<Controller.TroopManagerBase>().Attack(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_coordinateMapper.GetTroopAt(_endCoords[0], _endCoords[1]));

		return result ? OK : ERROR;
	}

	private string Build()
	{
		ExtractCoordsFromCommand();
		var buildingName = Command.Parameters.Building;

		if (_startCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null) { return ERROR; }

		bool result = Controller.GameManager.Get<Controller.BuildingManagerBase>().Build(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_buildingFactory.Instantiate(buildingName));

		return result ? OK : ERROR;
	}

	private string EndTurn()
	{
		Controller.GameManager.Get<Controller.TurnManagerBase>().FinishTurn();
		return OK;
	}

	private string Learn()
	{
		string techName = Command.Parameters.Tech;

		var currentPlayer = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer;
		if (!currentPlayer.Techs.ContainsKey(techName)) { return ERROR; }

		Model.TechTreeItemBase techItem = _techTreeItemFactory.Instantiate(techName);
		bool result = Controller.GameManager.Get<Controller.TechTreeManagerBase>().UnlockTech(techItem);

		return result ? OK : ERROR;
	}

	private string Move()
	{
		ExtractCoordsFromCommand();

		if (_startCoords.Length < 2 ||
			_endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null ||
			_coordinateMapper.GetTileAt(_endCoords[0], _endCoords[1]) is null) { return ERROR; }

		bool result = Controller.GameManager.Get<Controller.TroopManagerBase>().MoveTroop(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_coordinateMapper.GetTileAt(_endCoords[0], _endCoords[1]));

		return result ? OK : ERROR;
	}

	private string Train()
	{
		ExtractCoordsFromCommand();
		string troopName = Command.Parameters.Troop;

		if (_startCoords.Length < 2 ||
			_coordinateMapper.GetBuildingAt(_startCoords[0], _startCoords[1]) is null) { return ERROR; }

        bool result = Controller.GameManager.Get<Controller.TroopManagerBase>().Train(
			_coordinateMapper.GetBuildingAt(_startCoords[0], _startCoords[1]),
			_troopFactory.Instantiate(troopName));

		return result ? OK : ERROR;
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
