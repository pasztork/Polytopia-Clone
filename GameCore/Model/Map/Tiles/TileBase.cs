using System.Collections.Generic;

namespace Model
{
    public abstract class TileBase
    {
        public IList<TileBase> Neighbors { get; } = new List<TileBase>();
        public BuildingBase BuildingOnTop { get; set; }
        public TroopBase TroopOnTop { get; set; }

        public abstract bool SetBuildingOnTop(BuildingBase buildingOnTop, Player player);
        public abstract bool TrainTroop(TroopBase troop);
        public abstract bool AcceptTroop(TroopBase troop);
    }
}