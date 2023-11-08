using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class TurnManager : TurnManagerBase
    {
        private readonly LinkedList<Player> players = new LinkedList<Player>();
        private LinkedListNode<Player> playerNode;
        private int turns = 0;
        private int? maxTurns = null;
        private int MaxTurns
        {
            get => maxTurns ?? 50;
            set
            {
                maxTurns = value;
            }
        }

        public override void Start()
        {
            playerNode = players.First;
            turns++;
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
            playerNode = playerNode.Next ?? players.First;
            if(playerNode == players.First)
            {
                turns++;
                if(turns == MaxTurns)
                {
                    StopGameWithPoints();
                    return;
                }
            }
            CurrentPlayer = playerNode.Value;
            CurrentPlayer.StartTurn();
            RaiseOnTurnStarted(CurrentPlayer);
        }

        public override void PlayerCreated(Player player)
        {
            players.AddLast(player);
            player.OnEliminated += HandlePlayerEliminated;
        }

        public override void SetMaxTurns(int maxTurns)
        {
            MaxTurns = maxTurns;
        }

        private void HandlePlayerEliminated(Player player)
        {
            players.Remove(player);
            if(players.Count == 1)
            {
                StopGameWitElimination();
            }
        }

        private void StopGameWitElimination()
        {
            RaiseOnWinnerDecided(players.ElementAt(0));
            players.Clear();
        }

        private void StopGameWithPoints()
        {
            (Player, int) playerWithMaxResource = (players.First.Value, players.First.Value.ResourceContainer.AllCount);
            foreach(Player player in players)
            {
                if(player.ResourceContainer.AllCount > playerWithMaxResource.Item2) {
                    playerWithMaxResource = (player, player.ResourceContainer.AllCount);
                }
            }
            RaiseOnWinnerDecided(playerWithMaxResource.Item1);
            players.Clear();
        }
    }
}