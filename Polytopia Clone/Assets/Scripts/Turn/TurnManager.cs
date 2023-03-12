using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public Player CurrentPlayer { get; private set; }

    private readonly LinkedList<Player> players = new LinkedList<Player>();
    private LinkedListNode<Player> playerNode;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one TurnManager in scene!");
            return;
        }
        Instance = this;
    }

    public void NextPlayer()
    {
        CurrentPlayer.EndTurn();
        playerNode = playerNode.Next ?? players.First;
        CurrentPlayer = playerNode.Value;
        CurrentPlayer.StartTurn();
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
