using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceContainer))]
public class Player : MonoBehaviour
{
    [SerializeField] private string username;
    public string Username { get { return username; } }

    public IList<BuildingBase> AvailableBuildingBlueprints { get; private set; } = new List<BuildingBase>();

    private ResourceContainer resourceContainer;

    private void Awake()
    {
        resourceContainer = GetComponent<ResourceContainer>();
    }

    private void Start()
    {
        TurnManager.Instance.PlayerCreated(this);
        AvailableBuildingBlueprints.Add(BuildManager.Instance.BuildingBlueprints["Farm"]);
    }

    public void StartTurn()
    {
        Debug.Log($"{username}'s turn started!");
        resourceContainer.StartTurn();
    }

    public void EndTurn()
    {
        Debug.Log($"{username}'s turn ended!");
    }
}
