using System.Collections.Generic;

namespace LogView
{
    public class JsonDataHolder
    {
        public string Map { get; set; } = "";
        public List<JsonPlayerObject> Players { get; set; } = new List<JsonPlayerObject>();
        public List<JsonActionObject> Actions { get; set; } = new List<JsonActionObject>();
    }
}
