using System;

namespace Model
{
    public abstract class TrainManagerBase
    {
        public event Action<Player> OnTroopTrained;

        public abstract bool Train(BuildingBase building, TroopBase troop);

        protected void RaiseOnTroopTrained(Player player)
        {
            OnTroopTrained?.Invoke(player);
        }
    }
}