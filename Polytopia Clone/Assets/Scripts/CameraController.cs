using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    // Parameters
    [SerializeField] float panSpeed = 30f;
    [SerializeField] float minY = 10f;
    [SerializeField] float maxY = 80f;
    [SerializeField] float rotationSpeed = -2f;

    // Private fields
    Dictionary<string, Vector3> keyVectorPairs = new()
    {
        { "w", Vector3.forward },
        { "s", Vector3.back },
        { "d", Vector3.right },
        { "a", Vector3.left },
    };

    void Update()
    {
        Move();
        Rotate();
        ClampPosition();
    }

    void Move()
    {
        foreach (string key in keyVectorPairs.Keys)
        {
            if (Input.GetKey(key))
            {
                transform.Translate(keyVectorPairs[key] * panSpeed * Time.deltaTime, Space.Self);
            }
        }
    }

    void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            float rotationAroundYAxis = transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
            float rotationAroundXAxis = transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * rotationSpeed;
            rotationAroundXAxis = Mathf.Clamp(rotationAroundXAxis, 0, 90);
            transform.localRotation = Quaternion.Euler(rotationAroundXAxis, rotationAroundYAxis, 0);
        }
    }

    void ClampPosition()
    {
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = position;
    }
}
