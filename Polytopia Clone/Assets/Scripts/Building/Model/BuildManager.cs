using System;

namespace Model
{
    public class BuildManager
    {
        public static BuildManager instance;
        public static BuildManager Instance
        {
            get
            {
                instance ??= new BuildManager();
                return instance;
            }
        }

        public event Action<Player> OnBuildingBuilt;

        public bool Build(TroopBase troop, BuildingBase building)
        {
            bool built = TurnManager.Instance.CurrentPlayer.Build(troop, building);

            if (built)
            {
                building.Player = TurnManager.Instance.CurrentPlayer;
                OnBuildingBuilt?.Invoke(TurnManager.Instance.CurrentPlayer);
                LogDataWrapper.Instance.TriggerBuild(troop, building);
                LogDataWrapper.Instance.TriggerTroopDeath(troop);
            }

            return built;
        }

        
    }
}