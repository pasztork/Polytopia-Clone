using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class TurnManager
    {
        private static TurnManager instance;
        public static TurnManager Instance
        {
            get
            {
                instance ??= new TurnManager();
                return instance;
            }
        }

        public event Action<Player> OnTurnStarted;
        public event Action<Player> OnWinnerDecided;

        public Dictionary<string, int> CurrentActionCount { get; private set; }

        public Player CurrentPlayer { get; private set; }
        private readonly LinkedList<Player> players = new LinkedList<Player>();
        private LinkedListNode<Player> playerNode;

        public void Start()
        {
            playerNode = players.Last;
            CurrentPlayer = playerNode.Value;
            CurrentActionCount = CopyDictionary(CurrentPlayer.ActionCount);
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
            CurrentActionCount = CopyDictionary(CurrentPlayer.ActionCount);
            CurrentPlayer.StartTurn();
            OnTurnStarted?.Invoke(CurrentPlayer);
        }

        public void PlayerCreated(Player player)
        {
            players.AddFirst(player);
            player.OnEliminitad += HandlePlayerEliminated;
        }

        public Dictionary<string, int> CopyDictionary(Dictionary<string, int> original)
        {
            Dictionary<string, int> copy = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> kvp in original)
            {
                string keyCopy = string.Copy(kvp.Key);
                copy[keyCopy] = kvp.Value;
            }
            return copy;
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