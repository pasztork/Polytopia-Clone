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

        public IList<TechTreeItemBase> UnlockedTechsByCurrentPlayer
        {
            get
            {
                return TurnManager.Instance.CurrentPlayer.UnlockedTechs;
            }
        }
    }
}
