using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class TurnManager
    {
        public event Action<Player> OnTurnStarted;
        public event Action<Player> OnWinnerDecided;

        public Player CurrentPlayer { get; private set; }
        private readonly LinkedList<Player> players = new LinkedList<Player>();
        private LinkedListNode<Player> playerNode;

        public void Start()
        {
            playerNode = players.Last;
            CurrentPlayer = playerNode.Value;
            CurrentPlayer.StartTurn();
            OnTurnStarted?.Invoke(CurrentPlayer);
        }

        public void FinishTurn()
        {
            CurrentPlayer.EndTurn();
            StartTurn();
        }

        private void StartTurn()
        {
            playerNode = playerNode.Next ?? players.First;
            CurrentPlayer = playerNode.Value;
            CurrentPlayer.StartTurn();
            OnTurnStarted?.Invoke(CurrentPlayer);
        }

        public void PlayerCreated(Player player)
        {
            players.AddFirst(player);
            player.OnEliminited += HandlePlayerEliminated;
        }

        private void HandlePlayerEliminated(Player player)
        {
            players.Remove(player);
            if (players.Count == 1)
                StopGame();
        }

        private void StopGame()
        {
            OnWinnerDecided?.Invoke(players.ElementAt(0));
            players.Clear();
        }
    }
}