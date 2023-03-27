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
        public Player Player { get; set; }
        public LogActions LogActions { get; set; }
        public List<TileBase> Tiles { get; set; }
        public List<TroopBase> Troops { get; set; }
        public List<BuildingBase> Buildings { get; set; }

        //public TechController Tech { get; set; }
    }
}
