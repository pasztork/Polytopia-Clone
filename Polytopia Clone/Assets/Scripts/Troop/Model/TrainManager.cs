using System;

namespace Model
{
    public class TrainManager
    {
        public event Action<Player> OnTroopTrained;

        public bool Train(BuildingBase building, TroopBase troop)
        {
            bool trained = DependencyContainer.Get<TurnManager>().CurrentPlayer.Train(building, troop);

            if (trained)
            {
                troop.Tile = building.Tile;
                troop.Player = DependencyContainer.Get<TurnManager>().CurrentPlayer;
                OnTroopTrained?.Invoke(DependencyContainer.Get<TurnManager>().CurrentPlayer);
                LogDataWrapper.Instance.TriggerTrain(troop);
            }

            return trained;
        }
    }
}