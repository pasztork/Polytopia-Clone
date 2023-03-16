using UnityEngine;

[RequireComponent(typeof(ResourceContainer))]
public class Player : MonoBehaviour
{
    public string Username { get { return username; } }
    public BuildingBlueprintHolder BuildingBlueprintHolder { get { return buildingBlueprintHolder; } set { buildingBlueprintHolder = value; } }

    [SerializeField] private string username;

    // TODO: Use something else. ScrptableObject in here doesn't work.
    [SerializeField] private BuildingBlueprintHolder buildingBlueprintHolder;

    private ResourceContainer resourceContainer;

    private void Awake()
    {
        resourceContainer = GetComponent<ResourceContainer>();
    }

    private void Start()
    {
        TurnManager.Instance.PlayerCreated(this);
        BuildingBlueprintHolder = BuildManager.Instance.BuildingBlueprints;
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
