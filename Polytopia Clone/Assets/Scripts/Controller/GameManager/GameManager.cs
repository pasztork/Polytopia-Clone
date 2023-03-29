using System;
using Util;

namespace Controller
{
    public class GameManager
    {
        private static readonly GameManager instance = new GameManager();

        public static void NewGame(MapProperties mapProperites)
        {
            SetupMap(mapProperites);
        }

        public static void LoadGame()
        {
            throw new NotImplementedException();
        }

        public static void Start()
        {
            Model.DependencyContainer.Get<Model.GameManagerBase>().Start();
        }

        private static void SetupMap(MapProperties mapProperites)
        {
            Model.DependencyContainer.Get<Model.MapManagerBase>().Size = mapProperites.Size;
            Model.DependencyContainer.Get<Model.MapGeneratorBase>().MGP = mapProperites.GenerationProperties;
            Model.DependencyContainer.Get<Model.MapManagerBase>().GenerateMap();
        }

        public static T Get<T>()
        {
            return instance.dependencyContainer.Get<T>();
        }

        private readonly DependencyContainer dependencyContainer = new DependencyContainer();

        public GameManager()
        {
            dependencyContainer.Register<BuildingManagerBase, BuildingManager>();
        }
    }
}