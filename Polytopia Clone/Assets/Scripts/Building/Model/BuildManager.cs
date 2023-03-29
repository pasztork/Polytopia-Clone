using System;

namespace Model
{
    public class BuildManager : BuildManagerBase
    {
        
        public override bool Build(TroopBase troop, BuildingBase building)
        {
            bool built = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Build(troop, building);

            if (built)
            {
                building.Player = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer;
                RaiseOnBuildingBuilt(DependencyContainer.Get<TurnManagerBase>().CurrentPlayer);
            }

            return built;
        }
    }
}