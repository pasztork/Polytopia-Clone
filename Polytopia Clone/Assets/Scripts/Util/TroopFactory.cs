using Model;
using System;
using System.Collections.Generic;

namespace Util
{
    public class TroopFactory
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

        public TroopBase Instanciate(string type)
        {
            if (!_stringToTypeDictionary.ContainsKey(type))
            {
                throw new ArgumentOutOfRangeException($"No such troop exists: {type}");
            }

            Type troopType = _stringToTypeDictionary[type];
            return Activator.CreateInstance(troopType) as TroopBase;
        }
    }
}
