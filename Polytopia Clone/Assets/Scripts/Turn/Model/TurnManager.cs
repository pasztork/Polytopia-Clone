using System.Collections.Generic;
using System.Linq;
using View;

namespace Model
{
    public class TurnManager : TurnManagerBase
    {
        private readonly LinkedList<Player> players = new LinkedList<Player>();
        private LinkedListNode<Player> playerNode;

        public override void Start()
        {
            playerNode = players.Last;
            CurrentPlayer = playerNode.Value;
            JsonLogger js = JsonLogger.Instance;
            CurrentPlayer.StartTurn();
            RaiseOnTurnStarted(CurrentPlayer);
        }

        public override void FinishTurn()
        {
            CurrentPlayer.EndTurn();
            StartTurn();
        }

        private void StartTurn()
        {
            playerNode = playerNode.Next ?? players.First;
            CurrentPlayer = playerNode.Value;
            CurrentPlayer.StartTurn();
            RaiseOnTurnStarted(CurrentPlayer);
        }

        public override void PlayerCreated(Player player)
        {
            players.AddFirst(player);
            player.OnEliminated += HandlePlayerEliminated;
        }

        private void HandlePlayerEliminated(Player player)
        {
            players.Remove(player);
            if (players.Count == 1)
                StopGame();
        }

        private void StopGame()
        {
            RaiseOnWinnerDecided(players.ElementAt(0));
            players.Clear();
        }
    }
}