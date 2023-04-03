using LogView;
using Model;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace ReplayView
{
    public class ReloadManager : MonoBehaviour
    {
        [SerializeField]
        private string jsonLogFilePath;
        private ReloadManager instance;
        public ReloadManager Instance
        {
            get
            {
                instance ??= new ReloadManager();
                return instance;
            }
        }

        public void Awake()
        {
            string fileContent = File.ReadAllText(jsonLogFilePath);
            JsonDataHolder jsonDataHolder = JsonSerializer.Deserialize<JsonDataHolder>(fileContent);

            Model.GameManager.Get<MapManagerBase>().LoadMap(jsonDataHolder.Map);
            foreach (var player in jsonDataHolder.Players)
            {
                View.Player gamer = new View.Player();
                gamer.SetDefaultParams(player.StartingParamsFile);
                Instantiate(gamer);
            }
            ReplayModel.ReplayManager.InitializeGame(jsonDataHolder.Actions);
        }
    }
}
