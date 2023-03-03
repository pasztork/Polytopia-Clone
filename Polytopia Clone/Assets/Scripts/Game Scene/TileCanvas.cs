using UnityEngine;

public class TileCanvas : MonoBehaviour
{
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        GetComponent<Canvas>().worldCamera = mainCamera;
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
