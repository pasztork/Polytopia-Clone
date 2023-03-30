namespace Model
{
    public class CatapultTech : TechTreeItemBase
    {
        public CatapultTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //unlocks Catapult troop
        }
    }
}
