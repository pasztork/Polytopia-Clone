using UnityEngine;

public class Plane : MonoBehaviour
{
    private void OnMouseDown()
    {
        BuildManager.Instance.SelectedTile = null;
        BuildCanvas.Instance.Hide();
    }
}
