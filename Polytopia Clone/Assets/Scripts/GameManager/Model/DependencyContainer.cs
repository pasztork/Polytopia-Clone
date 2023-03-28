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
            Register<GameManager>();
            Register<MapManager>();
            Register<MapGenerator>();
            Register<TurnManager>();
            Register<BuildManager>();
            Register<TrainManager>();
            Register<TechTreeManager>();
        }

        public void Register<T>()
        {
            if (registeredObjects.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"Type {typeof(T)} is already registered");

            Type implementationType = typeof(T);
            registeredObjects[typeof(T)] = Activator.CreateInstance(implementationType);
        }
    }
}