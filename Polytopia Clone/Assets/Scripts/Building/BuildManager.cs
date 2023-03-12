using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    public Tile SelectedTile { get; set; }
    public ResourceContainer ActiveResourceContainer { get; set; } = null;

    [SerializeField] private GameObject buildingBlueprint;
    [SerializeField] private GameObject troopBlueprint;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        Instance = this;
    }

    public void Build()
    {
        Instantiate(buildingBlueprint, SelectedTile.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
        HideUI();
    }

    public void DeployTroop()
    {
        Instantiate(troopBlueprint, SelectedTile.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
        HideUI();
    }

    private void HideUI()
    {
        SelectedTile = null;
        BuildCanvas.Instance.InvokeEvent();
        BuildCanvas.Instance.Hide();
    }
}
