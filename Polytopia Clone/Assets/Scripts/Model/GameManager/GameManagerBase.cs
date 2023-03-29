using System.Collections.Generic;

namespace Model
{
    public abstract class GameManagerBase
    {
        public IList<Player> Players { get; } = new List<Player>();

        public abstract void Start();
    }
}