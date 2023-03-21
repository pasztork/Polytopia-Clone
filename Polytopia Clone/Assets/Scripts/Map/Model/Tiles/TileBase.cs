using System.Collections.Generic;

namespace Model
{
    public abstract class TileBase
    {
        public IList<TileBase> Neighbors { get; } = new List<TileBase>();
        public BuildingBase BuildingOnTop { get; protected set; }
        public TroopBase TroopOnTop { get; set; }

        public virtual bool SetBuildingOnTop(BuildingBase buildingOnTop) => false;
        public virtual bool TrainTroop(TroopBase troop) => false;
        public virtual bool AcceptTroop(TroopBase troop) => false;
    }
}