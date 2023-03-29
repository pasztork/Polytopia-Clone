namespace Model
{
    public class ArcheryTech : TechTreeItemBase
    {
        public ArcheryTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect()
        {
            //Unlock Archer troop
        }
    }
}
