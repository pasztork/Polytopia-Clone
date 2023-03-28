using System;

namespace Model
{
    public abstract class BuildManagerBase
    {
        public event Action<Player> OnBuildingBuilt;

        public abstract bool Build(TroopBase troop, BuildingBase building);

        protected void RaiseOnBuildingBuilt(Player player)
        {
            OnBuildingBuilt?.Invoke(player);
        }
    }
}