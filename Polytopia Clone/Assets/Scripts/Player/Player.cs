using UnityEngine;

[RequireComponent(typeof(ResourceContainer))]
public class Player : MonoBehaviour
{
    public string Username { get { return username; } }

    [SerializeField] private string username;

    private ResourceContainer resourceContainer;

    private void Awake()
    {
        resourceContainer = GetComponent<ResourceContainer>();
    }

    private void Start()
    {
        TurnManager.Instance.PlayerCreated(this);
    }

    public void StartTurn()
    {
        Debug.Log($"{username}'s turn started!");
        resourceContainer.StartTurn();
        Debug.Log(resourceContainer.ToString());
    }

    public void EndTurn()
    {
        Debug.Log($"{username}'s turn ended!");
    }
}
