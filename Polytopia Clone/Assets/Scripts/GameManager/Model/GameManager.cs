using System.Collections.Generic;

namespace Model
{
    public class GameManager
    {
        public IList<Player> Players { get; } = new List<Player>();

        public void Start()
        {
            foreach (Player player in Players)
                player.SetupStartingPosition();

            DependencyContainer.Get<TechTreeManager>().BuildTechTree();
            DependencyContainer.Get<TurnManager>().Start();
        }
    }
}