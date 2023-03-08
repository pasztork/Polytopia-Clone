using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Parameters
    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float minY = 10f;
    [SerializeField] private float maxY = 80f;
    [SerializeField] private float rotationSpeed = 2f;

    // Private fields
    private readonly Dictionary<string, Vector3> keyVectorPairs = new()
    {
        { "w", Vector3.forward },
        { "s", Vector3.back },
        { "d", Vector3.right },
        { "a", Vector3.left },
    };

    private void Update()
    {
        Move();
        Rotate();
        ClampPosition();
    }

    private void Move()
    {
        foreach (string key in keyVectorPairs.Keys)
        {
            if (Input.GetKey(key))
            {
                transform.Translate(keyVectorPairs[key] * panSpeed * Time.deltaTime, Space.Self);
            }
        }
    }

    private void Rotate()
    {
        // Only rotate camera if right mouse button is clicked
        if (Input.GetMouseButton(1))
        {
            float rotationAroundYAxis = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationAroundXAxis = transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * rotationSpeed;
            rotationAroundXAxis = Mathf.Clamp(rotationAroundXAxis, 0, 90);
            transform.localRotation = Quaternion.Euler(rotationAroundXAxis, rotationAroundYAxis, 0);
        }
    }

    private void ClampPosition()
    {
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = position;
    }
}
