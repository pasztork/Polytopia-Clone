using Util;
using ViewUtil;

namespace Network;

public class NetworkCommandProcessor : JsonCommandProcessorBase
{
    private readonly CoordinateToModelMapper _coordinateMapper = new();

    private readonly BuildingFactory _buildingFactory = new();

    private readonly TechTreeItemFactory _techTreeItemFactory = new();

    private readonly TroopFactory _troopFactory = new();

    private int[] startCoords = Array.Empty<int>();

    private int[] endCoords = Array.Empty<int>();

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
        Controller.GameManager.Get<Controller.BuildingManagerBase>().Attack(
            _coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]),
            _coordinateMapper.GetBuildingAt(endCoords[0], endCoords[1]));
    }

    private void AttackTroop()
    {
        ExtractCoordsFromCommand();
        Controller.GameManager.Get<Controller.TroopManagerBase>().Attack(
            _coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]),
            _coordinateMapper.GetTroopAt(endCoords[0], endCoords[1]));
    }

    private void Build()
    {
        ExtractCoordsFromCommand();
        string buildingName = Command.ActionDatas.Building;
        Controller.GameManager.Get<Controller.BuildingManagerBase>().Build(
            _coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]),
            _buildingFactory.Instantiate(buildingName));
    }

    private void EndTurn() =>
        Controller.GameManager.Get<Controller.TurnManagerBase>().FinishTurn();

    private void Learn()
    {
        string techName = Command.ActionDatas.Tech;
        Model.TechTreeItemBase techItem = _techTreeItemFactory.Instantiate(techName);
        Controller.GameManager.Get<Controller.TechTreeManagerBase>().UnlockTech(techItem);
    }

    private void Move()
    {
        ExtractCoordsFromCommand();
        Controller.GameManager.Get<Controller.TroopManagerBase>().MoveTroop(
            _coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]),
            _coordinateMapper.GetTileAt(endCoords[0], endCoords[1]));
    }

    private void Train()
    {
        ExtractCoordsFromCommand();
        string troopName = Command.ActionDatas.Troop;
        Controller.GameManager.Get<Controller.TroopManagerBase>().Train(
            _coordinateMapper.GetBuildingAt(startCoords[0], startCoords[1]),
            _troopFactory.Instantiate(troopName));
    }

    private void ExtractCoordsFromCommand()
    {
        startCoords = new int[] { Command.ActionDatas.Start[0], Command.ActionDatas.Start[1] };
        endCoords = new int[] { Command.ActionDatas.End[0], Command.ActionDatas.End[1] };
    }
}