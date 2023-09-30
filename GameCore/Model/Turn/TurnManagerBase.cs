using System;

namespace Model
{
    public abstract class TurnManagerBase
    {
        public event Action<Player> OnTurnStarted;
        public event Action<Player> OnWinnerDecided;

        public Player CurrentPlayer { get; protected set; }

        public abstract void Start();

        public abstract void FinishTurn();

        public abstract void PlayerCreated(Player player);

        protected void RaiseOnTurnStarted(Player player)
        {
            OnTurnStarted?.Invoke(player);
        }

        protected void RaiseOnWinnerDecided(Player player)
        {
            OnWinnerDecided?.Invoke(player);
        }
    }
}