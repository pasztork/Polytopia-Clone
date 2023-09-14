using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class TurnManager : TurnManagerBase
    {
        private readonly LinkedList<Player> players = new LinkedList<Player>();
        private LinkedListNode<Player> playerNode;

        public override void Start()
        {
            playerNode = players.First;
            CurrentPlayer = playerNode.Value;
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
            if (players.Count == 1)
            {
                StopGame();
                return;
            }

            playerNode = playerNode.Next ?? players.First;
            CurrentPlayer = playerNode.Value;
            CurrentPlayer.StartTurn();
            RaiseOnTurnStarted(CurrentPlayer);
        }

        public override void PlayerCreated(Player player)
        {
            players.AddLast(player);
            player.OnEliminated += HandlePlayerEliminated;
        }

        private void HandlePlayerEliminated(Player player)
        {
            players.Remove(player);
        }

        private void StopGame()
        {
            RaiseOnWinnerDecided(players.ElementAt(0));
            players.Clear();
        }

        public override void ReplayStopGame()
        {
            StopGame();
        }
    }
}