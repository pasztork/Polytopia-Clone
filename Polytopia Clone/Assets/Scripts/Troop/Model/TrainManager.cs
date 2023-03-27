using System;

namespace Model
{
    public class TrainManager
    {
        public static TrainManager instance;
        public static TrainManager Instance
        {
            get
            {
                instance ??= new TrainManager();
                return instance;
            }
        }

        public event Action<Player> OnTroopTrained;

        public bool Train(BuildingBase building, TroopBase troop)
        {
            bool trained = TurnManager.Instance.CurrentPlayer.Train(building, troop);

            if (trained)
            {
                troop.Tile = building.Tile;
                troop.Player = TurnManager.Instance.CurrentPlayer;
                OnTroopTrained?.Invoke(TurnManager.Instance.CurrentPlayer);
                LogDataWrapper.Instance.TriggerTrain(troop);
            }

            return trained;
        }
    }
}