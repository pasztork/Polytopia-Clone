using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceContainer))]
public class Player : MonoBehaviour
{
    [SerializeField] private string username;
    public string Username { get { return username; } }

    [SerializeField] private string[] startingBuildings;
    public IList<BuildingBase> AvailableBuildingBlueprints { get; private set; } = new List<BuildingBase>();

    [SerializeField] private string[] startingTroops;
    public IList<TroopBase> AvailableTroopBlueprints { get; private set; } = new List<TroopBase>();

    private ResourceContainer resourceContainer;

    private void Awake()
    {
        resourceContainer = GetComponent<ResourceContainer>();
    }

    private void Start()
    {
        TurnManager.Instance.PlayerCreated(this);
        foreach (string building in startingBuildings)
        {
            AvailableBuildingBlueprints.Add(BuildManager.Instance.BuildingBlueprints[building]);
        }

        foreach(string troop in startingTroops)
        {
            AvailableTroopBlueprints.Add(TrainManager.Instance.TroopBlueprints[troop]);
        }
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
