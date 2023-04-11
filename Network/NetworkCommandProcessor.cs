using ViewUtil;

namespace Network;

public class NetworkCommandProcessor : JsonCommandProcessorBase
{
    private readonly CoordinateToModelMapper _coordinateMapper = new();

    private int[] startCoords = Array.Empty<int>();
    private int[] endCoords = Array.Empty<int>();

    public NetworkCommandProcessor()
    {
        actions.Add("AttackBuilding", AttackBuilding);
        // TODO: add all remaining allowed commands to actions
    }

    private void AttackBuilding()
    {
        ExtractCoordsFromCommand();
        Controller.GameManager.Get<Controller.BuildingManagerBase>().Attack(
            _coordinateMapper.GetTroopAt(startCoords[0], startCoords[1]),
            _coordinateMapper.GetBuildingAt(endCoords[0], endCoords[1]));
    }

    private void ExtractCoordsFromCommand()
    {
        startCoords = new int[] { Command.ActionDatas.Start[0], Command.ActionDatas.Start[1] };
        endCoords = new int[] { Command.ActionDatas.End[0], Command.ActionDatas.End[1] };
    }
}
