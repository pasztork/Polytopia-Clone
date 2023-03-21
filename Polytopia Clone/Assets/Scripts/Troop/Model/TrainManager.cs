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
            if (TurnManager.Instance.CurrentActionCount["Train"] <= 0)
                return false;

            bool trained = TurnManager.Instance.CurrentPlayer.Train(building, troop);

            if (trained)
            {
                troop.Tile = building.Tile;
                troop.Player = TurnManager.Instance.CurrentPlayer;
                TurnManager.Instance.CurrentActionCount["Train"]--;
                OnTroopTrained?.Invoke(TurnManager.Instance.CurrentPlayer);
            }

            return trained;
        }
    }
}