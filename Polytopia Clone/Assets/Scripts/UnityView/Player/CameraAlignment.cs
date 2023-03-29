using UnityEngine;

namespace View
{
    public class CameraAlignment : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(
                mainCamera.transform.rotation.eulerAngles.x,
                mainCamera.transform.rotation.eulerAngles.y,
                mainCamera.transform.rotation.eulerAngles.z
            );
        }
    }
}