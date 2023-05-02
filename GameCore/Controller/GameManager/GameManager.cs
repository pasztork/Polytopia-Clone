using Util;

namespace Controller
{
    public class GameManager
    {
        private static readonly GameManager instance = new GameManager();

        public static void NewGame()
        {
            Model.GameManager.Get<Model.MapManagerBase>().GenerateMap();
        }

        public static void LoadGame()
        {
            throw new NotImplementedException();
        }

        public static void StartNew()
        {
            Model.GameManager.StartNew();
        }

        public static T Get<T>()
        {
            return instance.dependencyContainer.Get<T>();
        }

        private readonly DependencyContainer dependencyContainer = new DependencyContainer();

        public GameManager()
        {
            dependencyContainer.Register<TurnManagerBase, TurnManager>();
            dependencyContainer.Register<BuildingManagerBase, BuildingManager>();
            dependencyContainer.Register<TroopManagerBase, TroopManager>();
            dependencyContainer.Register<TechTreeManagerBase, TechTreeManager>();
        }
    }
}