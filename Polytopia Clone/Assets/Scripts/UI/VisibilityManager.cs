public class VisibilityManager
{
    public static void HideAll()
    {
        MapManager.Instance.SelectedTile = null;
        BuildManager.Instance.ActiveBuildingHolder = null;
    }
}
