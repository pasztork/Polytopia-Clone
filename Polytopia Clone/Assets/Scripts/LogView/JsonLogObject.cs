using System.Collections.Generic;

namespace View
{
    public class JsonLogObject
    {
        public string Map { get; set; }
        public IList<JsonPlayerObject> Players { get; set; }
        public IList<object> Actions { get; set; }
    }
}
