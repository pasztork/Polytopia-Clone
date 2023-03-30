using UnityEngine;

namespace View
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Controller.MapProperties mapProperties = new Controller.MapProperties()
            {
                Size = MapManager.Instance.Size,
                GenerationProperties = MapManager.Instance.GenerationProperties.ToModel()
            };
            JsonLogger.Instance.SetUpToLog();
            Controller.GameManager.NewGame(mapProperties);
        }

        private void Start()
        {
            LogDataWrapper.Instance.SubscribeToPlayerEvents();
            Controller.GameManager.Start();
        }
    }
}
