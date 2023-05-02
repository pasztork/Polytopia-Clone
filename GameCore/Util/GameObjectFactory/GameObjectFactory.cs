using System;
using System.Collections.Generic;

namespace Util
{
    public abstract class GameObjectFactory<TBaseClass>
    {
        protected Factory<TBaseClass> Factory { get; } = new();

        protected IDictionary<string, Func<TBaseClass>> StringToFuncDictionary { get; } =
            new Dictionary<string, Func<TBaseClass>>();

        public TBaseClass Instantiate(string typeName)
        {
            if (!StringToFuncDictionary.ContainsKey(typeName))
            {
                throw new ArgumentOutOfRangeException($"No such game object type: {typeName}");
            }

            Func<TBaseClass> creatorMethod = StringToFuncDictionary[typeName];
            return creatorMethod.Invoke();
        }
    }
}