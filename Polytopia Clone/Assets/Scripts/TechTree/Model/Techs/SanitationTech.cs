namespace Model
{
    public class SanitationTech : TechTreeItemBase
    {
        public SanitationTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //troops can heal
        }
    }
}
