using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static Tile selectedTile;

    [SerializeField] GameObject buildingBlueprint;
    [SerializeField] GameObject troopBlueprint;

    public void Build()
    {
        Debug.Log("Build");
        Instantiate(buildingBlueprint, selectedTile.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
    }

    public void DeployTroop()
    {
        Debug.Log("Deploy Troop");
        Instantiate(troopBlueprint, selectedTile.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
    }
}
