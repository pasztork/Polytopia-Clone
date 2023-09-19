using System.Collections.Generic;

namespace LogView
{
    public class JsonLogContent
    {
        public string Map { get; set; } = "";
        public List<JsonPlayerObject> Players { get; set; } = new();
        public List<JsonLog.JsonActionObject> Actions { get; set; } = new();
    }
}
