using Controller;
using System;
using System.Collections.Generic;

namespace View
{
    public class JsonDataHolder
    {
        public string Player { get; set; } = "";
        public string Action { get; set; } = "";
        public List<Identity> Tiles { get; set; } = new List<Identity>();
        public List<Identity> Troops { get; set; } = new List<Identity>();
        public List<Identity> Buildings { get; set; } = new List<Identity>();
        public string Tech { get; set; } = "";
    }
}
