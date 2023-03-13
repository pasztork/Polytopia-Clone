public class VisibilityManager
{
    public static void HideAll()
    {
        BuildManager.Instance.SelectedTile = null;
        BuildManager.Instance.ActiveBuildingHolder = null;

        BuildCanvas.Instance.InvokeEvent();
        BuildCanvas.Instance.Hide();
    }
}
