using UnityEngine;

public class Map : MonoBehaviour
{
    public void HideCanvases()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.GetChild(0).gameObject.SetActive(false);
        }
    }
}
