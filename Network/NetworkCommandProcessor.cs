using Util;
using ViewUtil;

namespace Network;

public class NetworkCommandProcessor : JsonCommandProcessorBase
{
	private readonly CoordinateToModelMapper _coordinateMapper = new();

	private readonly BuildingFactory _buildingFactory = new();

	private readonly TechTreeItemFactory _techTreeItemFactory = new();

	private readonly TroopFactory _troopFactory = new();

	private readonly int[] startCoords = new int[2];

	private readonly int[] endCoords = new int[2];

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

	private void AttackBuilding()
	{
		ExtractCoordsFromCommand();

		// Parameter check
		if (startCoords.Length < 2 ||
			endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]) is null ||
			_coordinateMapper.GetBuildingAt(endCoords[0], endCoords[1]) is null) { return; }

		Controller.GameManager.Get<Controller.BuildingManagerBase>().Attack(
			_coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]),
			_coordinateMapper.GetBuildingAt(endCoords[0], endCoords[1]));
	}

	private void AttackTroop()
	{
		ExtractCoordsFromCommand();

		// Parameter check
		if (startCoords.Length < 2 ||
			endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]) is null ||
			_coordinateMapper.GetTroopAt(endCoords[0], endCoords[1]) is null) { return; }

		Controller.GameManager.Get<Controller.TroopManagerBase>().Attack(
			_coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]),
			_coordinateMapper.GetTroopAt(endCoords[0], endCoords[1]));
	}

	private void Build()
	{
		ExtractCoordsFromCommand();
		var buildingName = Command.Parameters.Building;

		// Parameter check
		if (startCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]) is null) { return; }

		Controller.GameManager.Get<Controller.BuildingManagerBase>().Build(
			_coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]),
			_buildingFactory.Instantiate(buildingName));
	}

	private void EndTurn() =>
		Controller.GameManager.Get<Controller.TurnManagerBase>().FinishTurn();

	private void Learn()
	{
		string techName = Command.Parameters.Tech;

		// Parameter check
		var currentPlayer = Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer;
		if (!currentPlayer.Techs.ContainsKey(techName)) { return; }

		Model.TechTreeItemBase techItem = _techTreeItemFactory.Instantiate(techName);
		Controller.GameManager.Get<Controller.TechTreeManagerBase>().UnlockTech(techItem);
	}

	private void Move()
	{
		ExtractCoordsFromCommand();

		// Parameter check
		if (startCoords.Length < 2 ||
			endCoords.Length < 2 ||
			_coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]) is null ||
			_coordinateMapper.GetTileAt(endCoords[0], endCoords[1]) is null) { return; }

		Controller.GameManager.Get<Controller.TroopManagerBase>().MoveTroop(
			_coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]),
			_coordinateMapper.GetTileAt(endCoords[0], endCoords[1]));
	}

	private void Train()
	{
		ExtractCoordsFromCommand();
		string troopName = Command.Parameters.Troop;

		// Parameter check
		if (startCoords.Length < 2 ||
			_coordinateMapper.GetBuildingAt(startCoords[0], startCoords[1]) is null) { return; }

		Controller.GameManager.Get<Controller.TroopManagerBase>().Train(
			_coordinateMapper.GetBuildingAt(startCoords[0], startCoords[1]),
			_troopFactory.Instantiate(troopName));
	}

	private void ExtractCoordsFromCommand()
	{
		ExtractCoordsFromTo(Command.Parameters.Start, startCoords);
		ExtractCoordsFromTo(Command.Parameters.End, endCoords);
	}

	private static void ExtractCoordsFromTo(int[] from, int[] to)
	{
		if (from.Length < 2) { return; }

		to[0] = from[0];
		to[1] = from[1];
	}
}
