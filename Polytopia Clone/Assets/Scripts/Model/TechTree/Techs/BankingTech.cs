namespace Model
{
    public class BankingTech : TechTreeItemBase
    {
        public BankingTech(Cost cost) : base(cost)
        {
            HashCode = "Banking";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableBuildings.Add("Bank");
        }
    }
}
