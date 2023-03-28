using System.Collections.Generic;

namespace Model
{
    public class TechTreeItemBase
    {
        public TechTreeItemProperty TechTreeItemProperty { get; set; }
        private IList<TechTreeItemBase> requirements = new List<TechTreeItemBase>();
        public bool IsAvailable
        {
            get
            {
                foreach (TechTreeItemBase tech in requirements)
                {
                    if (!tech.IsAvailable)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
