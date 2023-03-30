using System.Collections.Generic;

namespace Model
{
    public abstract class TechTreeItemBase
    {
        public TechTreeItemProperty TechTreeItemProperty { get; set; }
        public string HashCode { get; protected set; }
        public IList<TechTreeItemBase> Requirements { get; set; } = new List<TechTreeItemBase>();

        public TechTreeItemBase(string name, Cost cost, string description)
        {
            TechTreeItemProperty = new TechTreeItemProperty(name, cost, description);
        }

        public bool IsAvailable
        {
            get
            {
                foreach (TechTreeItemBase tech in Requirements)
                {
                    if (!tech.TechTreeItemProperty.IsUnlocked)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public abstract void ActivateEffect(Player player);
    }
}
