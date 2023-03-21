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

        public bool Build(TileBase tile, BuildingBase building)
        {
            if (TurnManager.Instance.CurrentActionCount["Build"] <= 0)
                return false;

            bool built = TurnManager.Instance.CurrentPlayer.Build(tile, building);

            if (built)
            {
                TurnManager.Instance.CurrentActionCount["Build"]--;
                OnBuildingBuilt?.Invoke(TurnManager.Instance.CurrentPlayer);
            }

            return built;
        }
    }
}