namespace Model
{
    public class BankingTech : TechTreeItemBase
    {
        public BankingTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Banking";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableBuildings.Add("Bank");
        }
    }
}
