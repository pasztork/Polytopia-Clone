namespace Model
{
    public class TechTreeItemProperty
    {
        public Cost Cost { get; set; }
        public bool IsUnlocked { get; set; }

        public TechTreeItemProperty(Cost cost)
        {
            Cost = cost;
            IsUnlocked = false;
        }
    }
}
