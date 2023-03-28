using Assets.Scripts.Logger.Model;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class JsonDataHolder
    {
        public string Player { get; set; } = null;
        public LogActions Action { get; set; } = LogActions.Non;
        public List<Identity> Tiles { get; set; } = null;
        public List<Identity> Troops { get; set; } = null;
        public List<Identity> Buildings { get; set; } = null;

        //public TechController Tech { get; set; }
    }
}
