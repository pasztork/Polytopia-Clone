using System.Collections.Generic;

namespace Model
{
    public abstract class TechTreeItemBase
    {
        public TechTreeItemProperty TechTreeItemProperty { get; set; }
        public string HashCode { get; protected set; }
        public IList<TechTreeItemBase> Requirements { get; set; } = new List<TechTreeItemBase>();

        public TechTreeItemBase(Cost cost)
        {
            TechTreeItemProperty = new TechTreeItemProperty(cost);
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
        public virtual void ActivateEffect(Player player)
        {
            return;
        }
    }
}
