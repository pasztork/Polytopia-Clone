using System.Collections.Generic;

namespace Model
{
    public abstract class TileBase
    {
        public IList<TileBase> Neighbors { get; } = new List<TileBase>();
        public BuildingBase BuildingOnTop { get; protected set; }

        public virtual bool SetBuildingOnTop(BuildingBase buildingOnTop) => false;
    }
}