using System;
using System.Collections.Generic;

namespace Util
{
    public class DependencyContainer
    {
        private readonly Dictionary<Type, object> registeredObjects = new Dictionary<Type, object>();

        public void Register<TBaseClass, TImplementation>()
        {
            if (registeredObjects.ContainsKey(typeof(TBaseClass)))
                throw new InvalidOperationException($"Type {typeof(TBaseClass)} is already registered");

            Type implementationType = typeof(TImplementation);
            registeredObjects[typeof(TBaseClass)] = Activator.CreateInstance(implementationType);
        }

        public T Get<T>()
        {
            return (T)registeredObjects[typeof(T)];
        }
    }
}