using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    private LinkedList<Player> players = new LinkedList<Player>();
    private LinkedListNode<Player> playerNode;
    private Player currentPlayer;

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
        Debug.Log($"{currentPlayer.Username}'s turn ended");
        playerNode = playerNode.Next ?? players.First;
        currentPlayer = playerNode.Value;
        Debug.Log($"{currentPlayer.Username}'s turn started");
        // TODO: notify player to start turn
        // ...
    }

    public void PlayerCreated(Player createdPlayer)
    {
        players.AddFirst(createdPlayer);
        Initialize();
    }

    private void Initialize()
    {
        playerNode = players.First;
        currentPlayer = playerNode.Value;
    }

}
