namespace Model
{
    public class BankingTech : TechTreeItemBase
    {
        public BankingTech(string name, Cost cost, string description) : base(name, cost, description) { }

        public override void ActivateEffect(Player player)
        {
            //Unlocks Bank building
        }
    }
}
