using System;

namespace Model
{
    public class BuildManager
    {
        public event Action<Player> OnBuildingBuilt;

        public bool Build(TroopBase troop, BuildingBase building)
        {
            bool built = DependencyContainer.Get<TurnManager>().CurrentPlayer.Build(troop, building);

            if (built)
            {
                building.Player = DependencyContainer.Get<TurnManager>().CurrentPlayer;
                OnBuildingBuilt?.Invoke(DependencyContainer.Get<TurnManager>().CurrentPlayer);
                LogDataWrapper.Instance.TriggerBuild(troop, building);
                LogDataWrapper.Instance.TriggerTroopDeath(troop);
            }

            return built;
        }


    }
}