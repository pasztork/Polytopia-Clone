using System.IO;
using System.Text.Json;
using UnityEngine;

namespace ReplayView
{
    public class ReloadManager : MonoBehaviour
    {
        [SerializeField]
        private string jsonLogFilePath;
        private LogView.JsonDataHolder jsonDataHolder;
        private static ReloadManager instance;
        public static ReloadManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ReloadManager>();
                }
                return instance;
            }
        }

        public void Awake()
        {
            string fileContent = File.ReadAllText(jsonLogFilePath);
            jsonDataHolder = JsonSerializer.Deserialize<LogView.JsonDataHolder>(fileContent);

            Model.GameManager.Get<Model.MapManagerBase>().LoadMap(jsonDataHolder.Map);
            Model.GameManager.Get<Model.MapGeneratorBase>().ConnectLoadedMap();

            MapManager.Instance.OnViewMappedToModel += AddPlayers;
            MapBuilder.Instance.BuildMapGFX(Model.GameManager.Get<Model.MapManagerBase>().Tiles);

            SetupReplayManager();
        }

        private void AddPlayers()
        {
            foreach (var player in jsonDataHolder.Players)
            {
                var viewPlayer = new GameObject(player.Name);
                viewPlayer.AddComponent<Player>();
                viewPlayer.GetComponent<Player>().SetPlayerFromLog(player);
            }
        }

        private static void SetupReplayManager()
        {
            ReplayManager.Instance.SetActionList(Instance.jsonDataHolder.Actions);
        }
    }
}
