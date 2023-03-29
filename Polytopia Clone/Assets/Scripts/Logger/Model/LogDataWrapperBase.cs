using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logger.Model
{
    public abstract class LogDataWrapperBase
    {
        public event Action<JsonDataHolder> NewDataCreated;

        public void TriggerEvent(JsonDataHolder data)
        {
            NewDataCreated?.Invoke(data);
        }

        public abstract void TriggerTurnEnded();
        public abstract void TriggerAttacked(TroopBase attacker, BuildingBase targetBuilding, TroopBase targetTroop, TileBase targetedTile);
        public abstract void TriggerTroopMoved(TroopBase troop, TileBase from, TileBase target);
        public abstract void TriggerTrain(TroopBase troop);
        public abstract void TriggerBuildingDestroy(BuildingBase building);
        public abstract void TriggerTroopDeath(TroopBase troop);
        public abstract void TriggerBuild(BuildingBase building);
    }
}
