using UnityEngine;

public class BuildCanvas : MonoBehaviour
{
    public static BuildCanvas Instance { get; set; }

    [SerializeField] Vector3 offset;

    Camera mainCamera;

    public void MoveToTile(Tile tile)
    {
        gameObject.SetActive(true);
        transform.position = tile.transform.position + offset;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one BuildCanvas in scene!");
            return;
        }
        Instance = this;
    }

    void Start()
    {
        mainCamera = Camera.main;
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(
            mainCamera.transform.rotation.eulerAngles.x,
            mainCamera.transform.rotation.eulerAngles.y,
            mainCamera.transform.rotation.eulerAngles.z
        );
    }
}
