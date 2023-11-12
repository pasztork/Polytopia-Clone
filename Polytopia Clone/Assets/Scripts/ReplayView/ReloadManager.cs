using Assets.Scripts.ReplayView.Navigation;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace ReplayView
{
    public class ReloadManager : MonoBehaviour
    {
        private LogView.JsonLogContent jsonDataHolder;
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

        private void Awake()
        {
            string fileContent = File.ReadAllText(PathTransferer.Instance.LogFilePath);
            jsonDataHolder = JsonSerializer.Deserialize<LogView.JsonLogContent>(fileContent);

            Model.GameManager.Get<Model.MapManagerBase>().LoadMap(jsonDataHolder.Map);

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
