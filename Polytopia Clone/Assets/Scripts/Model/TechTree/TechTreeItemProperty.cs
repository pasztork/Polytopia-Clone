namespace Model
{
    public class TechTreeItemProperty
    {
        public string Name { get; set; }
        public Cost Cost { get; set; }
        public string Description { get; set; }
        public bool IsUnlocked { get; set; }

        public TechTreeItemProperty(string name, Cost cost, string description)
        {
            Name = name;
            Cost = cost;
            IsUnlocked = false;
            Description = description;
        }
    }
}
