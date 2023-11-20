using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPrefab;

        private void Awake()
        {
            Model.GameManager.Get<Model.MapGeneratorBase>().NoiseFunction = PerlinNoise.GenerateNoiseMap;
            Controller.GameManager.NewGame();
        }

        private void Start()
        {
            CreatePlayers(PlayerTransferer.Instance.PlayerNames, PlayerTransferer.Instance.PlayerColors);
            Controller.GameManager.StartNew();
        }

        private void CreatePlayers(List<string> playerNames, List<Color> colors)
        {
            for (int i = 0; i < playerNames.Count; i++)
            {
                Player player = Instantiate(playerPrefab).GetComponent<Player>();
                player.name = playerNames[i];
                player.playerColor = colors[i];
                player.SetupPlayer();
            }
        }

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
