using UnityEngine;

public class Player : MonoBehaviour
{
    public string Username { get { return username; } }

    [SerializeField] private string username;

    private void Start()
    {
        TurnManager.Instance.PlayerCreated(this);
    }
}
