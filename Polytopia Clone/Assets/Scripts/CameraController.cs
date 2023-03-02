using UnityEngine;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    // Parameters
    [SerializeField] float panSpeed = 30f;
    [SerializeField] float scrollSpeed = 5f;
    [SerializeField] float minY = 10f;
    [SerializeField] float maxY = 80f;

    // Private fields
    bool doMovement = true;
    Dictionary<string, Vector3> keyVectorPairs = new()
    {
        { "w", Vector3.forward },
        { "s", Vector3.back },
        { "d", Vector3.right },
        { "a", Vector3.left },
    };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (!doMovement)
        {
            return;
        }

        foreach (string key in keyVectorPairs.Keys)
        {
            if (Input.GetKey(key))
            {
                transform.Translate(keyVectorPairs[key] * panSpeed * Time.deltaTime, Space.World);
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
