using Controller;
using JsonLog;
using LogView.LogTransformer;
using Model;
using System.Collections.Generic;
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
				Health = troop.TroopProperty.Health,
				Damage = troop.TroopProperty.Damage,
				TilesToMove = GetTilesToMove(troop),
				EnemiesToAttack = GetEnemies(troop),
				BuildingsToBuild = GetBuildableBuildings(player, troop)
			});
        }
		return troops;
    }

	private int[] GetTilesToMove(TroopBase troop)
	{
		if (troop.MovedInTurn)
		{
			return new int[0];
		}
		return _coordinateMapper.GetCoordinatesOf(troop.TilesInMovementRange);
    }

	private List<EnemyState> GetEnemies(TroopBase troop)
	{
		if(troop.TroopProperty.AttackRange == 0)
		{
			return new();
		}

        List<EnemyState> enemies = new();
        foreach (TileBase tile in troop.TilesInAttackRange)
        {
            if (tile.TroopOnTop is not null)
            {
                var enemy = tile.TroopOnTop;
                enemies.Add(new EnemyState
                {
                    Name = enemy.Player.Name,
                    Type = enemy.ToString(),
                    Health = enemy.TroopProperty.Health,
                    Damage = enemy.TroopProperty.Damage,
                    Coordinates = new[] {
                            _coordinateMapper.GetCoordinatesOf(tile).Item1,
                            _coordinateMapper.GetCoordinatesOf(tile).Item2
                        }
                });
            }
        }
		return enemies;
    }

	private List<(string, CostState)> GetBuildableBuildings(Player player, TroopBase troop)
	{
		if(troop.Tile.BuildingOnTop is not null) 
		{
			return new();
		}

        List<(string, CostState)> buildings = new();
        foreach (var buildingName in player.AvailableBuildings)
		{
			Cost buildingCost = Cost.CreateNewFromJsonCost(BuildingBase.BuildingProperties[buildingName].Cost);
            if (player.ResourceContainer.HasEnoughFor(buildingCost) && troop.Buildings.Contains(buildingName))
			{
				buildings.Add((buildingName, new CostState
				{
					FoodCost = buildingCost.FoodCost,
					MaterialCost = buildingCost.MaterialCost,
					MoneyCost = buildingCost.MoneyCost
				}));
			}
		}
		return buildings;
	}

	private List<BuildingState> GetBuildings(Player player)
	{
		throw new NotImplementedException();
	}

    private List<TechState> GetTechs(Player player)
    {
        throw new NotImplementedException();
    }

    private void AttackBuilding()
	{
		ExtractCoordsFromCommand();

		if (_startCoords.Length < 2 ||
			_endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null ||
			_coordinateMapper.GetBuildingAt(_endCoords[0], _endCoords[1]) is null) { return; }

		Controller.GameManager.Get<Controller.BuildingManagerBase>().Attack(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_coordinateMapper.GetBuildingAt(_endCoords[0], _endCoords[1]));
	}

	private void AttackTroop()
	{
		ExtractCoordsFromCommand();

		if (_startCoords.Length < 2 ||
			_endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null ||
			_coordinateMapper.GetTroopAt(_endCoords[0], _endCoords[1]) is null) { return; }

		Controller.GameManager.Get<Controller.TroopManagerBase>().Attack(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_coordinateMapper.GetTroopAt(_endCoords[0], _endCoords[1]));
	}

	private void Build()
	{
		ExtractCoordsFromCommand();
		var buildingName = Command.Parameters.Building;

		if (_startCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null) { return; }

		Controller.GameManager.Get<Controller.BuildingManagerBase>().Build(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_buildingFactory.Instantiate(buildingName));
	}

	private void EndTurn() =>
		Controller.GameManager.Get<Controller.TurnManagerBase>().FinishTurn();

	private void Learn()
	{
		string techName = Command.Parameters.Tech;

		var currentPlayer = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer;
		if (!currentPlayer.Techs.ContainsKey(techName)) { return; }

		Model.TechTreeItemBase techItem = _techTreeItemFactory.Instantiate(techName);
		Controller.GameManager.Get<Controller.TechTreeManagerBase>().UnlockTech(techItem);
	}

	private void Move()
	{
		ExtractCoordsFromCommand();

		if (_startCoords.Length < 2 ||
			_endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]) is null ||
			_coordinateMapper.GetTileAt(_endCoords[0], _endCoords[1]) is null) { return; }

		Controller.GameManager.Get<Controller.TroopManagerBase>().MoveTroop(
			_coordinateMapper.GetTroopAt(_startCoords[0], _startCoords[1]),
			_coordinateMapper.GetTileAt(_endCoords[0], _endCoords[1]));
	}

	private void Train()
	{
		ExtractCoordsFromCommand();
		string troopName = Command.Parameters.Troop;

		if (_startCoords.Length < 2 ||
			_coordinateMapper.GetBuildingAt(_startCoords[0], _startCoords[1]) is null) { return; }

		Controller.GameManager.Get<Controller.TroopManagerBase>().Train(
			_coordinateMapper.GetBuildingAt(_startCoords[0], _startCoords[1]),
			_troopFactory.Instantiate(troopName));
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
