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
            if (TurnManager.Instance.CurrentActionCount["Build"] <= 0)
                return false;

            bool built;
            if(building is TroopTrainingBuilding)
            {
                built = TurnManager.Instance.CurrentPlayer.Build(troop, building as TroopTrainingBuilding);
            }
            else
            {
                built = TurnManager.Instance.CurrentPlayer.Build(troop, building as NonTrainingBuilding);
            }

            if (built)
            {
                building.Player = TurnManager.Instance.CurrentPlayer;
                TurnManager.Instance.CurrentActionCount["Build"]--;
                OnBuildingBuilt?.Invoke(TurnManager.Instance.CurrentPlayer);
            }

            return built;
        }
    }
}