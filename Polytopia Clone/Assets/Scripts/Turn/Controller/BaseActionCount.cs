using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    [CreateAssetMenu]
    public class BaseActionCount : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<string, int> baseActionCount;

        public Dictionary<string, int> CreateDictionary()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> kvp in baseActionCount)
            {
                string keyCopy = string.Copy(kvp.Key);
                int valueCopy = kvp.Value;
                dictionary[keyCopy] = valueCopy;
            }
            return dictionary;
        }
    }
}