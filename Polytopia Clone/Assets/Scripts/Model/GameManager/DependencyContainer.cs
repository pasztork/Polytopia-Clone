using System;
using System.Collections.Generic;

namespace Model
{
    public class DependencyContainer
    {
        private static DependencyContainer instance = new DependencyContainer();

        public static T Get<T>()
        {
            return (T)instance.registeredObjects[typeof(T)];
        }

        private readonly Dictionary<Type, object> registeredObjects = new Dictionary<Type, object>();

        public DependencyContainer()
        {
            Register<GameManagerBase, GameManager>();
            Register<MapManagerBase, MapManager>();
            Register<MapGeneratorBase, MapGenerator>();
            Register<TurnManagerBase, TurnManager>();
            Register<BuildManagerBase, BuildManager>();
            Register<TrainManagerBase, TrainManager>();
            Register<TechTreeManagerBase, TechTreeManager>();
        }

        public void Register<TBaseClass, TImplementation>()
        {
            if (registeredObjects.ContainsKey(typeof(TBaseClass)))
                throw new InvalidOperationException($"Type {typeof(TBaseClass)} is already registered");

            Type implementationType = typeof(TImplementation);
            registeredObjects[typeof(TBaseClass)] = Activator.CreateInstance(implementationType);
        }
    }
}