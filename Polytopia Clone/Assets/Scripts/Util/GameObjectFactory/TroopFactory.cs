using Model;
using System;
using System.Collections.Generic;

namespace Util
{
    public class TroopFactory : IGameObjectFactory<TroopBase>
    {
        private readonly IDictionary<string, Type> _stringToTypeDictionary = new Dictionary<string, Type>
        {
            { "Archer", typeof(Archer) },
            { "Boat", typeof(Boat) },
            { "Builder", typeof(Builder) },
            { "Catapult", typeof(Catapult) },
            { "Scout", typeof(Scout) },
            { "Settler", typeof(Settler) },
            { "Warrior", typeof(Warrior) }
        };

        public TroopBase Instantiate(string typeName)
        {
            if (!_stringToTypeDictionary.ContainsKey(typeName))
            {
                throw new ArgumentOutOfRangeException($"No such troop exists: {typeName}");
            }

            Type troopType = _stringToTypeDictionary[typeName];
            return Activator.CreateInstance(troopType) as TroopBase;
        }
    }
}
