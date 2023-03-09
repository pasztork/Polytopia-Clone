using UnityEngine;

public delegate void CanvasMovedDelegate();

public class BuildCanvas : MonoBehaviour
{
    public static BuildCanvas Instance { get; private set; }

    public event CanvasMovedDelegate CanvasMoved;
    [SerializeField] private Vector3 offset;
    private Camera mainCamera;

    public void MoveToTile(Tile tile)
    {
        gameObject.SetActive(true);
        transform.position = tile.transform.position + offset;
        CanvasMoved?.Invoke();

    }

    // TODO: This should not be a thing
    public void InvokeEvent()
    {
        CanvasMoved?.Invoke();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one BuildCanvas in scene!");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(
            mainCamera.transform.rotation.eulerAngles.x,
            mainCamera.transform.rotation.eulerAngles.y,
            mainCamera.transform.rotation.eulerAngles.z
        );

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
}
