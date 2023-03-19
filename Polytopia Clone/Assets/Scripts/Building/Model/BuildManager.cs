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
            if (tile.BuildingOnTop != null || !CanBuildOn(tile))
                return false;

            bool built = TurnManager.Instance.CurrentPlayer.Build(tile, building);

            if (built)
                OnBuildingBuilt?.Invoke(TurnManager.Instance.CurrentPlayer);

            return built;
        }

        private bool CanBuildOn(TileBase tile) =>
            tile.BuildingOnTop == null &&
            TurnManager.Instance.CurrentActionCount["Build"] > 0;


    }
}