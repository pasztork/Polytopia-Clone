using Model;
using System;
using System.Collections.Generic;

namespace Util
{
    public class BuildingFactory : IGameObjectFactory<BuildingBase>
    {
        private readonly IDictionary<string, Type> _stringToTypeDictionary = new Dictionary<string, Type>
        {
            { "Bank", typeof(Bank) },
            { "City", typeof(City) },
            { "Farm", typeof(Farm) },
            { "Harbor", typeof(Harbor) },
            { "Supplier", typeof(Supplier) }
        };

        public BuildingBase Instantiate(string typeName)
        {
            if (!_stringToTypeDictionary.ContainsKey(typeName))
            {
                throw new ArgumentOutOfRangeException($"No such building exists: {typeName}");
            }

            Type troopType = _stringToTypeDictionary[typeName];
            return Activator.CreateInstance(troopType) as BuildingBase;
        }
    }
}
