using System.Collections.Generic;

namespace Model
{
    public class TileBase
    {
        public IList<TileBase> Neighbors { get; } = new List<TileBase>();
    }
}