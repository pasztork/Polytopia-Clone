using System;

namespace Model
{
    public class BuildManager : BuildManagerBase
    {
        
        public override bool Build(TroopBase troop, BuildingBase building)
        {
            bool built = GameManager.Get<TurnManagerBase>().CurrentPlayer.Build(troop, building);

            if (built)
            {
                building.Player = GameManager.Get<TurnManagerBase>().CurrentPlayer;
                RaiseOnBuildingBuilt(GameManager.Get<TurnManagerBase>().CurrentPlayer);
            }

            return built;
        }
    }
}