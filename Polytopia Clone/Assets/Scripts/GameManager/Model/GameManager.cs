using System.Collections.Generic;

namespace Model
{
    public class GameManager
    {
        public static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                instance ??= new GameManager();
                return instance;
            }
        }

        public IList<Player> Players { get; } = new List<Player>();

        public void Start()
        {
            foreach (Player player in Players)
                player.SetupStartingPosition();
            TurnManager.Instance.Start();
        }
    }
}