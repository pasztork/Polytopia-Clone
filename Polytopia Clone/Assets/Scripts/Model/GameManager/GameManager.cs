using System.Collections.Generic;
using Util;

namespace Model
{
    public class GameManager
    {
        private static readonly GameManager instance = new GameManager();
        public static IList<Player> Players { get; } = new List<Player>();

        public static void StartNew()
        {
            foreach (Player player in Players)
                player.SetupStartingPosition();
            instance.dependencyContainer.Get<TurnManagerBase>().Start();
        }

        public static T Get<T>()
        {
            return instance.dependencyContainer.Get<T>();
        }

        private readonly DependencyContainer dependencyContainer = new DependencyContainer();

        public GameManager()
        {
            dependencyContainer.Register<MapManagerBase, MapManager>();
            dependencyContainer.Register<MapGeneratorBase, MapGenerator>();
            dependencyContainer.Register<TurnManagerBase, TurnManager>();
            dependencyContainer.Register<BuildManagerBase, BuildManager>();
            dependencyContainer.Register<TrainManagerBase, TrainManager>();
            dependencyContainer.Register<TechTreeManagerBase, TechTreeManager>();
        }
    }
}