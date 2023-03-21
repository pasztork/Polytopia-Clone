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
            if (!CanPutTroopOn(building.Tile))
                return false;

            bool trained = TurnManager.Instance.CurrentPlayer.Train(building, troop);

            if (trained)
                OnTroopTrained?.Invoke(TurnManager.Instance.CurrentPlayer);

            return trained;
        }

        private bool CanPutTroopOn(TileBase tile)
        {
            return tile.TroopOnTop == null &&
                    TurnManager.Instance.CurrentActionCount["Train"] > 0;
        }
    }
}