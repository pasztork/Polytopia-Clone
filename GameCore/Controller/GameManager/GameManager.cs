using Util;

namespace Controller
{
    public class GameManager
    {
        private static readonly GameManager instance = new();

        public static void NewGame() =>
            Model.GameManager.Get<Model.MapManagerBase>().GenerateMap();

        /// <summary>
        /// Should be used when no NoiseFunction is available.
        /// E.g. you don't have access to Unity's PerlinNoise.
        /// </summary>
        /// <param name="filePath">
        /// Path to the map one wants to use.
        /// </param>
        public static void NewGameWithSavedMap(string filePath) =>
            Model.GameManager.Get<Model.MapManagerBase>().LoadMap(filePath);

        public static void StartNew() => Model.GameManager.StartNew();

        public static T Get<T>() => instance.dependencyContainer.Get<T>();

        private readonly DependencyContainer dependencyContainer = new();

        private GameManager()
        {
            dependencyContainer.Register<TurnManagerBase, TurnManager>();
            dependencyContainer.Register<BuildingManagerBase, BuildingManager>();
            dependencyContainer.Register<TroopManagerBase, TroopManager>();
            dependencyContainer.Register<TechTreeManagerBase, TechTreeManager>();
        }
    }
}