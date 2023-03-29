namespace Model
{
    public class NavigationTech : TechTreeItemBase
    {
        public NavigationTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect()
        {
            //+1 movement range to water troops
        }
    }
}
