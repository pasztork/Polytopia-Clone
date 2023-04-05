using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogView
{
    public class JsonActionObject
    {
        public string Action { get; set; } = "";
        public JsonActionDatas ActionDatas { get; set; } = new JsonActionDatas();
    }
}
