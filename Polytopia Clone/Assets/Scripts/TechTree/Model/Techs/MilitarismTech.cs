namespace Model
{
    public class MilitarismTech : TechTreeItemBase
    {
        public MilitarismTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //increases troops attack damage
        }
    }
}
