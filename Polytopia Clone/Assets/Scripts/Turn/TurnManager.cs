using System.Collections.Generic;
using UnityEngine;

public delegate void TurnEventDelegate();

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    [SerializeField] private BaseActionCount baseActionTracker;

    // Used to track how many actions a player can take in their turn.
    // These should be copied at the beginning of the turn,
    // to track what the player did and be able to still know what he can do.
    private Dictionary<Player, Dictionary<string, int>> possibleActionsPerPlayer;
    public Dictionary<string, int> CurrentPossibleActions { get; private set; }

    // Should mostly be used by UI elements and not core game components
    // Can lead to unexpected behavior if not used so
    public event TurnEventDelegate StartTurn;

    public Player CurrentPlayer { get; private set; }

    private readonly LinkedList<Player> players = new LinkedList<Player>();
    private LinkedListNode<Player> playerNode;
    private bool started = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TurnManager in scene!");
            return;
        }
        Instance = this;
        possibleActionsPerPlayer = new Dictionary<Player, Dictionary<string, int>>();
    }

    private void Update()
    {
        if (!started)
        {
            CurrentPlayer.StartTurn();
            StartTurn?.Invoke();
            started = true;
        }
    }

    public void NextPlayer()
    {
        VisibilityManager.HideAll();

        CurrentPlayer.EndTurn();
        playerNode = playerNode.Next ?? players.First;
        CurrentPlayer = playerNode.Value;
        CurrentPlayer.StartTurn();
        CurrentPossibleActions = CopyDictionary(possibleActionsPerPlayer[CurrentPlayer]);

        StartTurn?.Invoke();
    }

    public void PlayerCreated(Player createdPlayer)
    {
        players.AddFirst(createdPlayer);
        possibleActionsPerPlayer.Add(createdPlayer, baseActionTracker.CreateDictionary());
        Initialize();
    }

    private void Initialize()
    {
        playerNode = players.Last;
        CurrentPlayer = playerNode.Value;
        BuildManager.Instance.ActiveResourceContainer =
            CurrentPlayer.GetComponent<ResourceContainer>();
        CurrentPossibleActions = CopyDictionary(possibleActionsPerPlayer[CurrentPlayer]);
    }

    private Dictionary<string, int> CopyDictionary(Dictionary<string, int> original)
    {
        Dictionary<string, int> copy = new Dictionary<string, int>();
        foreach (KeyValuePair<string, int> kvp in original)
        {
            string keyCopy = string.Copy(kvp.Key);
            int valueCopy = kvp.Value;
            copy[keyCopy] = valueCopy;
        }
        return copy;
    }

}
