using Assets.Scripts.Logger.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LogManager : LogManagerBase
    {
        //public LogManager() 
        //{
        //    DependencyContainer.Get<LogDataWrapper>().NewDataCreated += DependencyContainer.Get<LogManager>().TriggerEvent;
        //}

        public override int IncrementBuildId() => BuildId++;
        public override int IncrementTroopId() => TroopId++;
        public override int IncrementTileId() => TileId++;
    }
}
