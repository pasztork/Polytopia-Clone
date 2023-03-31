using System;
using System.Collections.Generic;
using Util;

namespace Model
{
    public static class GameManager
    {
        public static event Action<IList<Player>, string> OnGameStarted;

        public static IList<Player> Players { get; } = new List<Player>();
        private static readonly DependencyContainer dependencyContainer = new DependencyContainer();

        static GameManager()
        {
            dependencyContainer.Register<MapManagerBase, MapManager>();
            dependencyContainer.Register<MapGeneratorBase, MapGenerator>();
            dependencyContainer.Register<TurnManagerBase, TurnManager>();
            dependencyContainer.Register<BuildManagerBase, BuildManager>();
            dependencyContainer.Register<TrainManagerBase, TrainManager>();
            dependencyContainer.Register<TechTreeManagerBase, TechTreeManager>();
        }

        public static void StartNew()
        {
            foreach (Player player in Players)
                player.SetupStartingPosition();
            dependencyContainer.Get<TurnManagerBase>().Start();

            OnGameStarted?.Invoke(Players, Get<MapManagerBase>().MapFilePath);
        }

        public static T Get<T>()
        {
            return dependencyContainer.Get<T>();
        }


    }
}