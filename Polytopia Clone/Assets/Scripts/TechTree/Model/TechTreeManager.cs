using System.Collections.Generic;

namespace Model
{
    public class TechTreeManager
    {
        private static TechTreeManager instance;
        public static TechTreeManager Instance
        {
            get
            {
                instance ??= new TechTreeManager();
                return instance;
            }
        }

        public IList<TechTreeItemBase> TechsOfCurrentPlayer
        {
            get
            {
                return TurnManager.Instance.CurrentPlayer.Techs;
            }
        }
    }
}
