using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using View;

namespace LogView
{
    public class JsonPlayerParams
    {
        public int StartingCityRange { get; set; }
        public string[] StartingBuildings {get; set; }
        public string[] StartingTroops { get; set; }
        public List<JsonStartingProductionObject> BaseProduction { get; set; }
        public double[] PlayerColor { get; set; }
    }
}
