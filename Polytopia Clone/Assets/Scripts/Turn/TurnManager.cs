using System.Collections.Generic;
using UnityEngine;

public delegate void TurnEventDelegate();

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

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
        CurrentPlayer.EndTurn();
        playerNode = playerNode.Next ?? players.First;
        CurrentPlayer = playerNode.Value;
        CurrentPlayer.StartTurn();

        StartTurn?.Invoke();
    }

    public void PlayerCreated(Player createdPlayer)
    {
        players.AddFirst(createdPlayer);
        Initialize();
    }

    private void Initialize()
    {
        playerNode = players.First;
        CurrentPlayer = playerNode.Value;
        BuildManager.Instance.ActiveResourceContainer =
            CurrentPlayer.GetComponent<ResourceContainer>();
    }

}
