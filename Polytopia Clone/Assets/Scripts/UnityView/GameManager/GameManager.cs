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
            Controller.GameManager.NewGame(mapProperties);
        }

        private void Start()
        {
            Controller.GameManager.Start();
        }
    }
}
