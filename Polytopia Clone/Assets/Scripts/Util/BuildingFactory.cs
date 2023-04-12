using Model;
using System;
using System.Collections.Generic;

namespace Util
{
    public class BuildingFactory
    {
        private readonly IDictionary<string, Type> _stringToTypeDictionary = new Dictionary<string, Type>
        {
            { "Bank", typeof(Bank) },
            { "City", typeof(City) },
            { "Farm", typeof(Farm) },
            { "Harbor", typeof(Harbor) },
            { "Supplier", typeof(Supplier) }
        };

        public BuildingBase Instanciate(string type)
        {
            if (!_stringToTypeDictionary.ContainsKey(type))
            {
                throw new ArgumentOutOfRangeException($"No such building exists: {type}");
            }

            Type troopType = _stringToTypeDictionary[type];
            return Activator.CreateInstance(troopType) as BuildingBase;
        }
    }
}
