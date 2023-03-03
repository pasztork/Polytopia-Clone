using UnityEngine;

public class Tile : MonoBehaviour
{
    // Parameters
    [SerializeField] Color highlightColor;

    // Private fields
    Renderer rend;
    Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        rend.material.color = highlightColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
