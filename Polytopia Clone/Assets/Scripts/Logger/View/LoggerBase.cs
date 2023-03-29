using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public abstract class LoggerBase
    {
        public abstract void LogToFile(Model.JsonDataHolder dataHolder);
    }
}
