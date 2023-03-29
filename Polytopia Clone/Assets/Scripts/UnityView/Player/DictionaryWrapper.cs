using System.Collections.Generic;
using UnityEngine;

namespace View
{
    [CreateAssetMenu]
    public class DictionaryWrapper : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<string, int> serializableDictionary;

        public Dictionary<string, int> CreateDictionary()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> kvp in serializableDictionary)
            {
                string keyCopy = string.Copy(kvp.Key);
                int valueCopy = kvp.Value;
                dictionary[keyCopy] = valueCopy;
            }
            return dictionary;
        }
    }
}